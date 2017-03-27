using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithGPS : MonoBehaviour {

	public static float speed = 5850;

	public GameObject GPSobj;
	private GPSControl gpsControl;

	public GameObject cameraContainer;
	private GameObject cameraObject;
	private Camera camera;
	private Rigidbody rbContainer;

	private Vector3 curGPS;
	private Vector3 prevGPS;
	private bool firstGPSreading = true;

	// Use this for initialization
	void Start () {
		gpsControl = GPSobj.GetComponent<GPSControl> ();
		cameraObject = cameraContainer.transform.GetChild (0).gameObject;
		camera = cameraObject.GetComponent<Camera> ();
		rbContainer = cameraContainer.GetComponent<Rigidbody> ();
		rbContainer.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		float curLat = gpsControl.latitude;
		float curLon = gpsControl.longitude;
		float curAlt = gpsControl.altitude;
		if ((curLat == 0) || (curLon == 0)/*(curLat == 0) && (curLon == 0) && (curAlt == 0)*/) {
			return;
		}
		if (firstGPSreading) {
			prevGPS = new Vector3 (curLat, 0, curLon);
			curGPS = new Vector3 (curLat, 0, curLon);
			firstGPSreading = false;
			return;
		}
		prevGPS = curGPS;
		curGPS = new Vector3 (curLat, 0, curLon);
		Vector3 delta = curGPS - prevGPS;
		float deltaLen = delta.magnitude;
		deltaLen *= 100000;
		deltaLen *= speed;
		//cameraObject.transform.Translate (cameraObject.transform.forward * deltaLen * speed);
		rbContainer.AddForce(cameraObject.transform.forward * deltaLen);

	}//update


}//class
