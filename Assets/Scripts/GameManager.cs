using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//declare a singleton to access variables of the GameManager script
	public static GameManager S;

	//declaration of variables
	public GameObject[] cards;
	public int count = 0;

	public Text scoreText;
	public Text timerText;

	private float timer = 0;
	private float startTime;
	public float finalTime;
	public string minutes;
	public string seconds; 

	public bool selected;
	public int totalScore = 1000;
	public int totalPairs;

	public GameObject losingSound;

	//set the singleton
	void Awake(){
		S = this;
	}
		
	void Start () {
		//get the start time of the scene
		startTime = Time.time;

		//set the initial text in the gameplay scene
		SetText ();

		//call the function to shuffle the set of cards
		Shuffle (cards);

		//call the function to spawn cards 
		SpawnCards ();

		//get the total number of pairs of cards
		totalPairs = cards.Length / 2;
	}

	void Update(){
		//update the timer
		timer = Time.time - startTime;

		//set the updated text to the scene 
		SetText ();

		//check the outcome of the game whether the player lost or won 
		CheckGameOutcome ();
	}

	//create a new random variable 
	private static System.Random rand = new System.Random();
	private void Shuffle(GameObject [] list){
		
		int n = list.Length ;
		while (n > 1) {
			n--;
			int k = rand.Next (n); //get a random number 
			GameObject card = list [k]; //set a temp card to be the random card
			list [k] = list [n]; //set the random number
			list [n] = card; //put the temp card back in
		}

	}

	private void SpawnCards(){

		//set variables to adjust card distances 
		int k = 0;
		int j = 0;
		for (int i = 0; i < 18; i++) {
			if (i != 0 && i % 6 == 0) {
				j += -4; //after every 6 make a new row 4 pixels below
				k = 0; //reset the x-padding
			}
			Instantiate (cards [i], new Vector3 (-8.0f + (3.5f * k), 5f + j, -5f), Quaternion.identity);

			//increase the padding multiplier until 6 straight have been spawned
			k++;
		}
	}

	//create an array of cards that holds which card has been selected
	public GameObject [] selectedCards = new GameObject [2];

	public void AddCard(GameObject card){
		//if the first spot is null; the whole array is empty and add the card into the first spot
		if (selectedCards [0] == null)	 {
			selectedCards [0] = card;
		} else {
				selectedCards [1] = card; //if the first spot isn't null add the next card to the second spot
		}
	}

	//check the cards to see if they're the same 
	public void CheckCard(){

		//if both spots are not null 
		if (selectedCards [0] != null && selectedCards [1] != null) {
			if (selectedCards[0].tag != selectedCards[1].tag) {
				//if not the same set the selected to true so the rotate script knows the array is full 
				//when they're not flipped the selected is false again
				selected = true;
				Invoke ("CardsNotMatching", 0.5f);

			} else {

				//if they're matching call the matching function 
				Invoke ("CardsMatching", 0.5f);
			}
		} 
	}

	private void CardsNotMatching(){

		//create the game sound 
		Instantiate (losingSound, this.transform.position, this.transform.rotation);

		//set the game objects back to their original position
		selectedCards[0].transform.Rotate (new Vector3 (0, 180, 0));
		selectedCards[1].transform.Rotate (new Vector3 (0, 180, 0));

		//set the array back to null
		selectedCards [0] = null;
		selectedCards [1] = null;
		selected = false; //have the selecetd to false so it can be clicked again

		//decrease the score if wrong
		if (totalScore > 0) {

			totalScore -= 40;
		} 
	}
		
	private void CardsMatching(){

		//destroy the selected game objects if they're the same 
		Destroy (selectedCards[0]);
		Destroy (selectedCards[1]);

		totalPairs--; //decrease the total pairs to tell if the game is over
	}
		
	private void CheckGameOutcome(){
		//check the game outcome booleans 
		if (totalPairs == 0 & totalScore > 0) {
			finalTime = timer - startTime; 
			SceneManager.LoadScene ("Game_Over");

		} else if(totalScore <= 0){
			finalTime = timer - startTime;
			SceneManager.LoadScene ("Game_Over");
		}
	}

	private void SetText(){
		//set the text of each thing 
		minutes = ((int)timer / 60).ToString (); //get the minutes
		seconds = (timer % 60).ToString ("f1"); //get the seconds and have only 1 decimal place 
		scoreText.text = "Score: " + totalScore.ToString ();
		timerText.text = "Time: " + minutes + " :" + seconds;
	}

}
