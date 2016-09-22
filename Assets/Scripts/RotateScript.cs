using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
	
	public GameObject card;
	public GameObject flipSound;


	void Update(){
		if (GameManager.S.count > 1) {

			//check the card script and reset the count to 0 if two clicks have occurred
			GameManager.S.CheckCard ();
			GameManager.S.count = 0;
		}

	}

	void OnMouseDown(){
		//if two cards havent been selected allow the click to happen
		if (!GameManager.S.selected) {
			if (GameManager.S.count < 2) {

				//rotate the card if max. 2 clicks have been done
				card.transform.Rotate (new Vector3 (0, 180, 0));
				GameManager.S.count++;
				GameManager.S.AddCard (card);
				Instantiate (flipSound, this.transform.position, this.transform.rotation);

			}
		}
	}


}