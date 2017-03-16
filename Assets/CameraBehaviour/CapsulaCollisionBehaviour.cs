using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulaCollisionBehaviour : MonoBehaviour {


	public void OnCollisionEnter(Collision collision) {
		GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
}
