using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyRotate : MonoBehaviour {

	[SerializeField]
	float speed = 200;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.left * Time.deltaTime * speed);
	}
}
