using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithGPS : MonoBehaviour {

	public static float speed = 10000;

	public GameObject GPSobj;
	private GPSControl gpsControl;

	public GameObject cameraContainer;
	private GameObject cameraObject;
	private Camera camera;
	//private Rigidbody rbContainer;
	private Rigidbody rbCamera;

	private Vector3 curGPS;
	private Vector3 prevGPS;
	private bool firstGPSreading = true;

	// Use this for initialization
	void Start () {
		gpsControl = GPSobj.GetComponent<GPSControl> ();
		cameraObject = cameraContainer.transform.GetChild (0).gameObject;
		camera = cameraObject.GetComponent<Camera> ();
		rbCamera = cameraObject.GetComponent<Rigidbody> ();
		rbCamera.freezeRotation = true;
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
		//Vector3 targetPosition = cameraContainer.transform.position + delta * speed;
		Vector3 transformVec = new Vector3(0, 0, deltaLen * speed);
		//cameraObject.transform.Translate (transformVec, Space.Self);
		//rb.AddForce(0,0,-deltaLen * speed);
		rbCamera.AddRelativeForce(transformVec);

	}//update


}//class
