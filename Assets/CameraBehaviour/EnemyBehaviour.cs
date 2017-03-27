using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject Followee;
	public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		//Followee = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Followee != null) {
			transform.position = Vector3.MoveTowards (transform.position, Followee.transform.position, speed);
		} else {
			Debug.logger.Log (gameObject.name + ": my followee is null!!!");
		}
	}

	public void OnCollisionEnter(Collision collision) {
		DestroyObject (this.gameObject);
	}

	public void SetFollowee(GameObject flw) {
		Followee = flw;
	}

}
