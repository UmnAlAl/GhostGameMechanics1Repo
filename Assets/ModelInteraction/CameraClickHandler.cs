using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClickHandler : MonoBehaviour {

	Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray;
		if (SystemInfo.deviceType == DeviceType.Handheld) { // on mobile devices
			if (Input.touches.Length > 0) { //check if touches happened
				Touch touch = Input.GetTouch(0); //get only first touch, IMPROVE?
				if (touch.phase == TouchPhase.Began) { //if new touch started started
					ray = camera.ScreenPointToRay (touch.position);
				} else {
					return;
				}
			} else { //else stop
				return;
			}
		} else { //on PC
			if (Input.GetMouseButtonDown (0)) { //if new left button click
				ray = camera.ScreenPointToRay (Input.mousePosition); //transform coords of mouse to ray
			} else {
				return;
			}
		}

		//we have platform independent ray now
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)) {
			GameObject collideedObject = hit.transform.gameObject; //get object that has been hit
			collideedObject.SendMessage ("onRay");
		}

		//Debug.DrawRay (ray.origin, ray.direction * 10, Color.green, 5);

	} //Update

}
