using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour {

	public Text text;
	
	// Update is called once per frame
	void Update () {
		text.text = " Lat: " + GPSControl.Instance.latitude.ToString ()
			+ " Long: " + GPSControl.Instance.longitude.ToString ()
			+ " Alt: " + GPSControl.Instance.altitude.ToString ();
	}

}//class
