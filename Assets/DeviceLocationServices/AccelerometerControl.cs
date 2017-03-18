using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerControl : MonoBehaviour {

	public GameObject objToControl;
	private Rigidbody rb;
	float speed = 500F;

	// Use this for initialization
	void Start () {
		rb = objToControl.GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acc = Input.acceleration;
		//rb.AddForce(acc.x * speed, 0, acc.y * speed);
		rb.AddForce(acc.x * speed, acc.y * speed, acc.z * speed);
	}

}
