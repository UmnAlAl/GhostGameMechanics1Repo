using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStaticController : MonoBehaviour {

	public float MoveSpeed = 10f;
	public float RotSpeed = 10f;
	public float ZoomSpeed = 2f;
	public float MinFieldOfView = 15f;
	public float MaxFieldOfView = 90f;
	public GameObject obj;

	public void onButtonClickStaticController(string id) {
		switch (id) {
		case "MoveUp":
			move (Vector3.up);
			break;
		case "MoveDown":
			move (Vector3.down);
			break;
		case "MoveLeft":
			move (Vector3.left);
			break;
		case "MoveRight":
			move (Vector3.right);
			break;
		case "MoveFront":
			move (Vector3.forward);
			break;
		case "MoveBack":
			move (Vector3.back);
			break;
		case "LookLeft":
			rotate (Vector3.up, -1);
			break;
		case "LookRight":
			rotate (Vector3.up, 1);
			break;
		case "LookUp":
			rotate (Vector3.left, 1);
			break;
		case "LookDown":
			rotate (Vector3.left, -1);
			break;
		case "ZoomPlus":
			zoom (-1);
			break;
		case "ZoomMinus":
			zoom (1);
			break;
		}//switch

	}//func

	void move(Vector3 movDir) {
		Rigidbody rb = obj.GetComponent<Rigidbody> ();
		Vector3 curPos = obj.transform.position;
		Vector3 newPos = Vector3.zero;
		movDir.Scale(new Vector3(MoveSpeed, MoveSpeed, MoveSpeed));
		newPos = curPos + movDir;
		rb.MovePosition (newPos);
	}

	void rotate(Vector3 axis, float angle) {
		Vector3 curPos = obj.transform.position;
		angle *= RotSpeed;
		obj.transform.RotateAround(curPos, axis, angle);
	}

	void zoom(float direction) {
		Camera camera = obj.transform.GetChild(0).gameObject.GetComponent<Camera>();
		float fieldOfView = camera.fieldOfView;
		fieldOfView += direction * ZoomSpeed;
		fieldOfView = Mathf.Clamp (fieldOfView, MinFieldOfView, MaxFieldOfView);
		camera.fieldOfView = fieldOfView;
	}

	void Start() {
		Rigidbody rb = obj.GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
	}

}//class
