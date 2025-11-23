using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
	public float force_amount = 8;
	private Rigidbody2D rb;
	public float k=0.3f; // dry friction coefficient
	public float dashIntensity=10.0f;
	public float deltaDash=5.0f; // time between dashes
	private float timeSinceLastDash=0f;
	private bool dashAvailable=true;
	public float dashDuration=0.5f;
	private Vector2 direction = Vector2.zero;
	private Vector2 dashDirection = Vector2.zero;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		rb = GetComponent<Rigidbody2D>();
       	}
	// Update is called once per frame
	void Update() {
		direction = new Vector2(0,0);
		// We set current direction vector according to user's inputs
		if (Input.GetKey(KeyCode.UpArrow))
        {
            direction.y = 1;
        } if (Input.GetKey(KeyCode.DownArrow)) {
			direction.y = -1;
		} if (Input.GetKey(KeyCode.LeftArrow)) {
			direction.x = -1;
		} if (Input.GetKey(KeyCode.RightArrow)) {
			direction.x = 1;
		}
		// Normalising direction
		direction = direction.normalized;
		Debug.Log(direction);
		if (!dashAvailable) {
			// If the dash is not available, we count time until it will be
			timeSinceLastDash+=Time.deltaTime;
			if (timeSinceLastDash>deltaDash) {
				// If it has been enough time, dash is available 
				dashAvailable=true;
			}
		}
		if (Input.GetKeyDown(KeyCode.F) && dashAvailable) {
			// If users asks for a dash and it is available, we dash
			dashAvailable=false;
			timeSinceLastDash=0f;
			dashDirection=direction;
		}
	}
	void FixedUpdate() {
		if (timeSinceLastDash<=dashDuration) {
			rb.AddForce(dashDirection * dashIntensity, ForceMode2D.Impulse);
		}
		rb.AddForce(direction*force_amount);
		rb.AddForce(-k*rb.linearVelocity);
	}
}
