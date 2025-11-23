using UnityEngine;

public class SmallObstacle: MonoBehaviour {
		private Rigidbody2D rb;
		private float lifetime;
		public void Initialize(Vector3 direction, float speed, float lifetime) {
				rb = GetComponent<Rigidbody2D>();
				if (rb != null) {
						rb.linearVelocity = direction.normalized * speed;
				}
				Destroy(gameObject, lifetime);
		}
}
