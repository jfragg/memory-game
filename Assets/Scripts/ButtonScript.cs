using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	//declaration of variables 
	public Canvas quitMenu;

	void Start(){

		//set the quit panel to disabled 
		quitMenu.GetComponent<Canvas> ();
		quitMenu.enabled = false;
	}


	public void PlayPress(){
		//load the next scene if it player continues 
		SceneManager.LoadScene ("_Scene0");
	}

	public void ExitPress(){
		//if exit is chosen show the exit panel
		quitMenu.enabled = true;
	}

	public void NoPress(){
		//if no is selected disable the panel and go back to the main scene
		quitMenu.enabled = false;
	}

	public void YesPress(){
		//if yes is selected close the application
		Application.Quit ();
	}
}
