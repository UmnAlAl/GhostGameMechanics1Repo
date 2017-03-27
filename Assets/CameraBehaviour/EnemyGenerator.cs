using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

	public GameObject EnemyPrefab;
	public GameObject Followee;

	// Use this for initialization
	void Start () {
		StartCoroutine (generateEnemies ());
	}
	
	// Update is called once per frame
	IEnumerator generateEnemies() {
		while (true) {
			GameObject newEnemy =  Instantiate (EnemyPrefab) as GameObject;
			newEnemy.AddComponent<EnemyBehaviour> ();
			EnemyBehaviour beh = newEnemy.GetComponent<EnemyBehaviour>();
			beh.SetFollowee (Followee);

			//Debug.logger.Log ("Followee real: " + Followee.ToString ());
			//Debug.logger.Log ("Followee created: " + newEnemy.GetComponent<EnemyBehaviour>().Followee);

			yield return new WaitForSecondsRealtime (3);
		}
	}
}
