using UnityEngine;
using System.Collections;
using System;

public class Move : MonoBehaviour {
	Vector3 _pointToRotateAround;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
	}
	
	// Update is called once per frame
	void Update () {
		var rb = GetComponent<Rigidbody2D> ();
		//foreach(var touch in Input.touches) {
		if(Input.GetMouseButton(0)) {
			if (_pointToRotateAround != default(Vector3)) {
				rb.transform.RotateAround (_pointToRotateAround, new Vector3 (0, 0, 1), 5);
			} else {
				rb.rotation += 5;
			}
		}
		if (Input.GetMouseButton (1)) {
			if (_pointToRotateAround != default(Vector3)) {
				rb.transform.RotateAround (_pointToRotateAround, new Vector3 (0, 0, 1), -5);
			} else {
				rb.rotation -= 5;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag.ToLower() == "finish") {
			Debug.Log ("you win!");
		}
		_pointToRotateAround = col.gameObject.transform.position;
		Debug.Log ("set point to " + _pointToRotateAround.x + " " + _pointToRotateAround.y + " " + _pointToRotateAround.z);
	}
}