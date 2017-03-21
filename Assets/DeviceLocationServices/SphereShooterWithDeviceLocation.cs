﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereShooterWithDeviceLocation : MonoBehaviour {

	GameObject prefab;
	public GameObject cameraObject;

	// Use this for initialization
	void Start () {
		prefab = Resources.Load ("bullet") as GameObject;
	}

	public void onShootClick() {
		Camera camera = cameraObject.GetComponent<Camera>();
		GameObject newBullet = Instantiate (prefab) as GameObject;
		newBullet.transform.position = cameraObject.transform.position + camera.transform.forward * 2;
		Rigidbody rb = newBullet.GetComponent<Rigidbody> ();
		rb.velocity = camera.transform.forward * 40;
	}

}
