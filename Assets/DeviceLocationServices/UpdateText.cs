using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour {

	public Text text;
	public Text text2;
	public Text text3;
	public Text text4;
	public GameObject gyroControlObj;
	private GyroControl gyroControl;

	void Start() {
		gyroControl = gyroControlObj.GetComponent<GyroControl> ();
	}

	// Update is called once per frame
	void Update () {
		text.text = "GPS Lat: " + GPSControl.Instance.latitude.ToString ()
			+ " Long: " + GPSControl.Instance.longitude.ToString ()
			+ " Alt: " + GPSControl.Instance.altitude.ToString ();
		if (gyroControl.gyroEnabled) {
			text2.text = "Acceleration x: " + gyroControl.gyro.userAcceleration.x.ToString()
				+ " y: " + gyroControl.gyro.userAcceleration.y.ToString()
				+ " z: " + gyroControl.gyro.userAcceleration.z.ToString();
			text4.text = "El_attd: " + gyroControl.gyro.attitude.eulerAngles.ToString ()
			+ " El_lc_rot: " + (gyroControl.gyro.attitude * (new Quaternion (0, 0, 1, 0))).eulerAngles.ToString ();
			text3.text = "Move speed: " + MovementWithGPS.speed;
		}
	}

	public void OnSpeedChange(float delta) {
		//gyroControl.speed += delta;
		MovementWithGPS.speed += delta;
	}

}//class
