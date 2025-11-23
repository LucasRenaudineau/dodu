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
	private bool justDashed = false;
	public int playerID = 1;
	public void Initialize(int id) {
		playerID=id;
		SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
		if (sr != null) {
			if (playerID == 1) sr.color = Color.blue;
			if (playerID == 2) sr.color = Color.red;
			} else {
				Debug.LogError("Pas de SpriteRenderer trouvé dans les enfants !");
			}
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		rb = GetComponent<Rigidbody2D>();
       	}
	// Update is called once per frame
	void Update() {
		direction = new Vector2(0,0);
		// We set current direction vector according to user's inputs
		if (playerID==1) {
			if (Input.GetKey(KeyCode.UpArrow)) {
				direction.y = 1;
			} if (Input.GetKey(KeyCode.DownArrow)) {
				direction.y = -1;
			} if (Input.GetKey(KeyCode.LeftArrow)) {
				direction.x = -1;
			} if (Input.GetKey(KeyCode.RightArrow)) {
				direction.x = 1;
			}
		}
		else {
			if (Input.GetKey(KeyCode.W)) {
                                direction.y = 1;
                        } if (Input.GetKey(KeyCode.S)) {
                                direction.y = -1;
                        } if (Input.GetKey(KeyCode.A)) {
                                direction.x = -1;
                        } if (Input.GetKey(KeyCode.D)) {
                                direction.x = 1;
                        }
		}
		// Normalising direction
		direction = direction.normalized;
		if (!dashAvailable) {
			// If the dash is not available, we count time until it will be
			timeSinceLastDash+=Time.deltaTime;
			if (timeSinceLastDash>deltaDash) {
				// If it has been enough time, dash is available 
				dashAvailable=true;
			}
		}
		if (((Input.GetKeyDown(KeyCode.Keypad0) && playerID==1) || (Input.GetKeyDown(KeyCode.LeftShift) && playerID==2))&& dashAvailable) {
			// If users asks for a dash and it is available, we dash
			dashAvailable=false;
			timeSinceLastDash=0f;
			dashDirection=direction;
		}
	}
	void FixedUpdate() {
		if (timeSinceLastDash<=dashDuration) {
			rb.AddForce(dashDirection * dashIntensity, ForceMode2D.Impulse);
			justDashed=true;
		} else if (justDashed) {
			rb.linearVelocity=direction*force_amount;
			justDashed=false;
		} else {
			rb.AddForce(direction*force_amount);
			rb.AddForce(-k*rb.linearVelocity);
		}
	}
}
