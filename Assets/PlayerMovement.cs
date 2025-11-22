using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
	public float force_amount = 10;
	private Rigidbody2D rb;
	private int vertic = 0;
	private int horiz = 0;
	public float k=0.3f; // dry friction coefficient
	public float dashIntensity=10.0f;
	public float deltaDash=5.0f; // time between dashes
	private float timeSinceLastDash=0f;
	private bool dashAvailable=true;
	private bool justDashed=false;
	public float dashDuration=0.5f;
	private Vector2 dir = Vector2.zero;
	private Vector2 prevDir = Vector2.zero;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		timeSinceLastDash=deltaDash;
       	}
	// Update is called once per frame
	void Update() {
		vertic = 0;
		horiz = 0;
		if (Input.GetKey(KeyCode.UpArrow)) {
			vertic +=1;
		} if (Input.GetKey(KeyCode.DownArrow)) {
			vertic -=1;
		} if (Input.GetKey(KeyCode.LeftArrow)) {
			horiz -=1;
		} if (Input.GetKey(KeyCode.RightArrow)) {
			horiz +=1;
		}
		dir=(new Vector2(horiz,vertic)).normalized;
		if (dir != Vector2.zero && !dashAvailable) {
			timeSinceLastDash+=Time.deltaTime;
			if (timeSinceLastDash>deltaDash) {
				dashAvailable=true;
			}
		}
		if (Input.GetKeyDown(KeyCode.F) && dashAvailable) {
			dashAvailable=false;
			timeSinceLastDash=0f;
			prevDir=dir;
		}
	}
	void FixedUpdate() {
		if (timeSinceLastDash<=dashDuration) {
			rb.AddForce(prevDir * dashIntensity, ForceMode2D.Impulse);
			justDashed=true;
		}
		else if (justDashed) {
			rb.linearVelocity=dir*force_amount;
			justDashed=false;
		}
		else {
			rb.AddForce(dir*force_amount);
			rb.AddForce(-k*rb.linearVelocity);
		}
	}
}
