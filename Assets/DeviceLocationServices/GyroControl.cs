using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour {

	public bool gyroEnabled;
	public Gyroscope gyro;
	public float speed;
	public static float startMovingSlice = 0.02f;

	public GameObject cameraContainer;
	private GameObject cameraObject;
	private Camera camera;
	private Quaternion rot;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		gyroEnabled = EnableGyro ();
		cameraObject = cameraContainer.transform.GetChild (0).gameObject;
		camera = cameraObject.GetComponent<Camera> ();
		rb = cameraContainer.GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
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

			Vector3 moveVector = makeMovementFromAcceleration(gyro.userAcceleration);
			rb.AddForce (moveVector * speed);
		}
	}


	public static Quaternion GyroToUnityQuaternion(Quaternion q) {
		return new Quaternion (q.x, q.z, -q.y, -q.w);
	}


	private static Vector3 makeMovementFromAcceleration(Vector3 acceleration) {
		Vector3 moveVector = new Vector3 (0, 0, 0);

		if (checkCoordOutBounds(acceleration.x) && !checkCoordOutBounds(acceleration.y) && !checkCoordOutBounds(acceleration.z)) {
			moveVector.x = acceleration.x;
		}
		if (checkCoordOutBounds(acceleration.y) && !checkCoordOutBounds(acceleration.x) && !checkCoordOutBounds(acceleration.z)) {
			moveVector.y = acceleration.y;
		}
		if (checkCoordOutBounds(acceleration.z) && !checkCoordOutBounds(acceleration.x) && !checkCoordOutBounds(acceleration.y)) {
			moveVector.z = acceleration.z;
		}

		return moveVector;
	}


	private static bool checkCoordOutBounds(float coord) {
		if (!( (-startMovingSlice <= coord) && (coord <= startMovingSlice) )) {
			return true;
		}
		return false;
	}

}//class
