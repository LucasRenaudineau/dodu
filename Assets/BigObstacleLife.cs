using UnityEngine;

public class BigObstacleLife : MonoBehaviour {
		private Rigidbody2D rb;
		private float lifetime;
		public void Initialize(Vector3 direction, float speed, float destroyAfter) {
				rb = GetComponent<Rigidbody2D>();
				if (rb != null) {
						rb.linearVelocity = direction.normalized * speed;
				}
				lifetime = destroyAfter;
				Destroy(gameObject, lifetime);
		}
}
