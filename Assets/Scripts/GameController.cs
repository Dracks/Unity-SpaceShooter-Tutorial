using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	public GameObject player;
	public Transform  playerSpawnPosition;

	public GameObject gameOverPanel;

	public Vector3 spawnValues;
	private int waveCount;
	public int hazardBaseCount;
	public float startWait;
	public float spawnBaseWait;
	public float waveWait;
	private bool isGameOver = false;
	private int numLifes=3;


	public GUIText scoreText;
	private int score;
	private Coroutine spawning;

	void Start()
	{
		RestartGame ();
	}
	
	IEnumerator SpawnWaves()
	{
		float spawnWait = spawnBaseWait;
		int hazardCount = hazardBaseCount;
		yield return new WaitForSeconds(startWait);
		while (! isGameOver) 
		{
			for (int i=0; i< hazardCount; i++) {
					Vector3 spawnPosition = new Vector3 (
				Random.Range (-spawnValues.x, spawnValues.x), 
				0.0f, 
				spawnValues.z
						);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			spawnWait=spawnWait/1.5f;
			hazardCount= hazardCount*15/10;
			yield return new WaitForSeconds(waveWait);
		}
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score + "\nLifes: " + numLifes;
	}

	public void RestartGame(){
		isGameOver = false;
		spawning= StartCoroutine (SpawnWaves ());
		score = 0;
		numLifes = 3;
		gameOverPanel.SetActive(false);
		UpdateScore ();
		Instantiate (player, playerSpawnPosition.position, playerSpawnPosition.rotation);
	}

	public void LooseLive(){
		Debug.Log ("LoseLive");
		numLifes--;
		UpdateScore ();
		if (numLifes == 0) {
			GameOver ();
		} else {
			Instantiate (player, playerSpawnPosition.position, playerSpawnPosition.rotation);
		}
	}

	void GameOver(){
		isGameOver = true;
		gameOverPanel.SetActive(true);
		StopCoroutine (spawning);
	}
}
