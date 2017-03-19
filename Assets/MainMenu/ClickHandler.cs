using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour {

	public GameObject loadingImage;
	private const int quitLevel = 6;

	public void HandleButtonClick(int level){
		if (level.Equals (quitLevel)) {
			Application.Quit ();
			return;
		}
		loadingImage.SetActive (true);
		SceneManager.LoadScene (level);
	}

}
