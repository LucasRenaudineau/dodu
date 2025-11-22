using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigObstacleSpawner : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1000;
    [SerializeField] private float projectileLifetime = 50;
    [SerializeField] private float spawnInterval = 4;
	[SerializeField] private float initialDistance = 1000;
    private float timer = 0f;

    void Start() {
    }

    void SpawnProjectile() {
        float random_orientation = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(0, 0, random_orientation);
        
        float x = - Mathf.Cos(random_orientation * Mathf.Deg2Rad) * initialDistance;
        float y = - Mathf.Sin(random_orientation * Mathf.Deg2Rad) * initialDistance;
        Vector3 spawnPosition = new Vector3(x, y, 0) + transform.position;
        
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, rotation);
        
        Vector3 direction = (transform.position - spawnPosition).normalized;
        
        BigObstacleLife obstacleScript = projectile.GetComponent<BigObstacleLife>();
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
