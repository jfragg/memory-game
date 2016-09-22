using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	//declaration of variables 
	private float timeBeforeDestroy;
	public AudioSource sound;

	void Start(){
		//get the sound audio source 
		sound = this.GetComponent<AudioSource> ();
		timeBeforeDestroy = sound.clip.length; //set a time limit 

	}

	void Update(){
		timeBeforeDestroy -= Time.deltaTime; //set the timer

		//destroy the object if the time is less than or equal to 0
		if (timeBeforeDestroy <= 0f) {
			Destroy (this.gameObject);
		}
	}
}
