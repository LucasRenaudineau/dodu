using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
		public float force_amount = 10;
		private Rigidbody2D rb; // removed "public" - can't be both private and public
		private float keys_pressed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
			rb = GetComponent<Rigidbody2D>();
			keys_pressed = 0;
        
    }
    // Update is called once per frame
    void Update()
    {
			keys_pressed = 0;
			if (Input.GetKey(KeyCode.UpArrow)) {
					keys_pressed +=1;
		} if (Input.GetKey(KeyCode.DownArrow)) {
					keys_pressed +=1;
		} if (Input.GetKey(KeyCode.LeftArrow)) {
					keys_pressed +=1;
		} if (Input.GetKey(KeyCode.RightArrow)) {
					keys_pressed +=1;
		}
			if (keys_pressed==1) {
					if (Input.GetKey(KeyCode.UpArrow)) {
							rb.AddForce(new Vector2(0, force_amount), ForceMode2D.Impulse);
					} else if (Input.GetKey(KeyCode.DownArrow)) {
							rb.AddForce(new Vector2(0, -force_amount), ForceMode2D.Impulse);
					} else if (Input.GetKey(KeyCode.LeftArrow)) {
							rb.AddForce(new Vector2(-force_amount, 0), ForceMode2D.Impulse);
					} else if (Input.GetKey(KeyCode.RightArrow)) {
							rb.AddForce(new Vector2(force_amount, 0), ForceMode2D.Impulse);
					}
		}
		else if (keys_pressed>1) {
				if (Input.GetKey(KeyCode.UpArrow)) {
								rb.AddForce(new Vector2(0, force_amount/Mathf.Sqrt(2)), ForceMode2D.Impulse);
				} if (Input.GetKey(KeyCode.DownArrow)) { // Added closing parenthesis
								rb.AddForce(new Vector2(0, -force_amount/Mathf.Sqrt(2)), ForceMode2D.Impulse);
				} if (Input.GetKey(KeyCode.LeftArrow)) { // Added closing parenthesis
								rb.AddForce(new Vector2(-force_amount/Mathf.Sqrt(2), 0), ForceMode2D.Impulse);
				} if (Input.GetKey(KeyCode.RightArrow)) { // Added closing parenthesis
								rb.AddForce(new Vector2(force_amount/Mathf.Sqrt(2), 0), ForceMode2D.Impulse);
				}
		}
        
    }
}
