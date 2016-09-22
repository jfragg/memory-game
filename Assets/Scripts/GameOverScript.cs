using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	//declaration of variables 
	private int score;
	private float time;

	public Text scoreText;
	public Text outcomeText;
	public Text timeText;

	public GameObject winningSound;
	public GameObject gameLosingSound;

	public GameObject background1;
	public GameObject background2;

	// Use this for initialization
	void Start () {
		//set the score and time to that of the gamemanager before the scene is loaded 
		score = GameManager.S.totalScore;
		time = GameManager.S.finalTime;

		//set the winning and losing background screens to false
		background1.SetActive (false);
		background2.SetActive (false);

		//set the text of each item
		scoreText.text = "Score: " + score.ToString ();
		timeText.text = "Time: " + time.ToString ("f1");

		//if the player wins set the winning background, if loses set losing background 
		if (score > 0) {
			background1.SetActive (true);
			outcomeText.text = "YOU WIN!";
			Instantiate (winningSound, this.transform.position, this.transform.rotation);
		} else if (score == 0) {
			background2.SetActive (true);
			outcomeText.text = "YOU LOSE!";
			Instantiate (gameLosingSound, this.transform.position, this.transform.rotation);
		}

	}

	//if playagain button is clicked, load the main scene again with reshuffled cards
	public void PlayAgain(){
		SceneManager.LoadScene ("_Scene0");
	}
}
