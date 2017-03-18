using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSControl : MonoBehaviour {

	public float latitude;
	public float longitude;
	public float altitude;

	public static GPSControl Instance { get; set; }


	// Use this for initialization
	void Start () {
		Instance = this;
		DontDestroyOnLoad (gameObject);
		StartCoroutine (StartGPSService ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator StartGPSService() {
		if (!Input.location.isEnabledByUser) {
			yield break;
		}
		Input.location.Start ();
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		if (maxWait <= 0) {
			yield break;
		}

		if (Input.location.status == LocationServiceStatus.Failed) {
			yield break;
		}

		latitude = Input.location.lastData.latitude;
		longitude = Input.location.lastData.longitude;
		altitude = Input.location.lastData.altitude;

	}//func

}//class
