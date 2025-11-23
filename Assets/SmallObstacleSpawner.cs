using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallObstacleSpawner : MonoBehaviour {
		[SerializeField] private GameObject projectilePrefab;
		[SerializeField] private float projectileSpeed = 10000f;
		[SerializeField] private float projectileLifetime = 10f;
		[SerializeField] private float spawnInterval = 2f;
		[SerializeField] private float initialDistance = 3000f;
		[SerializeField] private float alea_zone = 1400f;
		private float timer = 0f;
		void Start()
		{
				SpawnProjectile();
		}

		void SpawnProjectile() {
        // The objects spawn from a circle of radius `intialDistance` with a random angle and home on the center
        float random_orientation = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(0, 0, random_orientation);
        float x = - Mathf.Cos(random_orientation * Mathf.Deg2Rad) * initialDistance + Random.Range(-alea_zone,alea_zone);
        float y = - Mathf.Sin(random_orientation * Mathf.Deg2Rad) * initialDistance + Random.Range(-alea_zone,alea_zone);
        Vector3 spawnPosition = new Vector3(x, y, 0) + transform.position;
        
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, rotation);
        
        Vector2 direction = (transform.position - spawnPosition).normalized;
        
        SmallObstacle obstacleScript = projectile.GetComponent<SmallObstacle>();
		if (obstacleScript != null) {
				obstacleScript.Initialize(direction, projectileSpeed, projectileLifetime);
		}
		}
		void Update() {
				timer += Time.deltaTime;
				if (timer >= spawnInterval) {
						SpawnProjectile();
						timer = 0;
				}
		}
}
