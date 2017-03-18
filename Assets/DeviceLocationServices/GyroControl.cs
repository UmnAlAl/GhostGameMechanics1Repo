using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour {

	private bool gyroEnabled;
	private Gyroscope gyro;

	public GameObject cameraContainer;
	private GameObject cameraObject;
	private Camera camera;
	private Quaternion rot;

	// Use this for initialization
	void Start () {
		gyroEnabled = EnableGyro ();
		cameraObject = cameraContainer.transform.GetChild (0).gameObject;
		camera = cameraObject.GetComponent<Camera> ();
	}
	
	private bool EnableGyro() {
		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;
			cameraContainer.transform.rotation = Quaternion.Euler (90f, 270f, 0f);
			rot = new Quaternion (0, 0, 1, 0);
			return true;
		}
		return false;
	}

	void Update () {
		if (gyroEnabled) {
			cameraObject.transform.localRotation = gyro.attitude * rot;
		}
	}

	public static Quaternion GyroToUnity(Quaternion q) {
		return new Quaternion (q.x, q.z, -q.y, -q.w);
	}

}//class
