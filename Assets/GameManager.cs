using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Header("UI Interfaces")]
	public GameObject startPanel;
	public GameObject gameOverPanel;
	[Header("Objects to activate when the game starts")]
	public GameObject[] objectsToActivate;
	private bool isGameRunning = false;
	private bool isGameOver = false;
	void Update() {
		if (!isGameRunning && Input.GetKeyDown(KeyCode.Return)) {
			StartGame();
		}
		if (isGameOver && Input.GetKeyDown(KeyCode.Return)) {
			RestartGame();
		}
	}
    	void StartGame() {
		isGameRunning = true;
		if(startPanel != null) startPanel.SetActive(false);
		foreach (GameObject obj in objectsToActivate) {
			if(obj != null)	obj.SetActive(true);
		}
	}
	public void TriggerGameOver() {
		if (isGameOver) return;
		isGameOver=true;
		isGameRunning=false;
		if (gameOverPanel != null) gameOverPanel.SetActive(true);
		foreach (GameObject obj in objectsToActivate) {
			if(obj != null) obj.SetActive(false);
                }
	}
	void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
