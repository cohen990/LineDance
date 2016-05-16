using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;

public class Move : MonoBehaviour {
	ControlBase controls = new MouseControl();
	Vector3 _centreOfRotation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var rb = GetComponent<Rigidbody2D> ();
		//foreach(var touch in Input.touches) {
		if(controls.isTurningCounterClockwise) {
			RotateRigidbody2D (5, _centreOfRotation);
		}
		if (controls.isTurningClockwise) {
			RotateRigidbody2D (-5, _centreOfRotation);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		SnapToCentre (col);
		if (col.gameObject.tag.ToLower() == "finish") {
			Debug.Log ("you win!");
		}
		_centreOfRotation = col.gameObject.transform.position;
		Debug.Log ("set point to " + _centreOfRotation.x + " " + _centreOfRotation.y + " " + _centreOfRotation.z);
	}

	private void SnapToCentre(Collider2D collider){
		var rb = GetComponent<Rigidbody2D> ();
		//var pos1 = collider..transform.position;
		var pos2 = collider.gameObject.transform.position;

	}

	private void RotateRigidbody2D(int angle, Vector3 centre = default(Vector3)){
		if (centre == default(Vector3)) {
			GetComponent<Rigidbody2D> ().rotation += angle;
		} else {
			GetComponent<Rigidbody2D> ().transform.RotateAround (centre, new Vector3 (0, 0, 1), angle);
		}
	}
}