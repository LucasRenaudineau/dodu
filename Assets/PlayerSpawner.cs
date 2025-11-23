using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	public GameObject playerPrefab;
	public Vector2 startPosP1 = new Vector2(-25, 0);
	public Vector2 startPosP2 = new Vector2(25, 0);
	public float scaleFactor = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	 void Start()
   	 {
		SpawnPlayers();
	 }

   	 // Update is called once per frame
   	 void SpawnPlayers() {
	 	GameObject p1 = Instantiate(playerPrefab, startPosP1, Quaternion.identity);
		p1.transform.localScale = Vector3.one * scaleFactor;
		PlayerMovement scriptP1 = p1.GetComponent<PlayerMovement>();
		scriptP1.Initialize(1);
		GameObject p2 = Instantiate(playerPrefab, startPosP2, Quaternion.identity);
		p2.transform.localScale = Vector3.one * scaleFactor;
                PlayerMovement scriptP2 = p2.GetComponent<PlayerMovement>();
                scriptP2.Initialize(2);
	 }
}
