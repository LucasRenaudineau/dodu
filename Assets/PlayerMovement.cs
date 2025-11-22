using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
	public float force_amount = 10;
	private Rigidbody2D rb; // removed "public" - can't be both private and public
	private float keys_pressed = 0;
	private int vertic = 0;
	private int horiz = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		rb = GetComponent<Rigidbody2D>();
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
		rb.AddForce((new Vector2(horiz,vertic)).normalized*force_amount, ForceMode2D.Impulse);
	}
}
