using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlaneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		WebCamTexture deviceCameraTexture = new WebCamTexture ();
		gameObject.GetComponent<Renderer> ().material.mainTexture = deviceCameraTexture;
		deviceCameraTexture.Play ();
	}

}
