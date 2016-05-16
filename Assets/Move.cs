using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;

public class Move : MonoBehaviour {
	private ControlBase controls = new MouseControl();
	private Vector3 _centreOfRotation;
	private float _speed = 4;


	// Use this for initialization
	void Start () {
		_centreOfRotation = GetComponent<Rigidbody2D> ().transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(controls.isTurningCounterClockwise) {
			RotateRigidbody2D (_speed, _centreOfRotation);
		}
		if (controls.isTurningClockwise) {
			RotateRigidbody2D (-_speed, _centreOfRotation);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log (GetComponent<Rigidbody2D>().transform.eulerAngles);
		SnapToCentre (col);
		if (col.gameObject.tag.ToLower() == "finish") {
			Debug.Log ("you win!");
		}
		_centreOfRotation = col.gameObject.transform.position;
		Debug.Log ("set point to " + _centreOfRotation.x + " " + _centreOfRotation.y + " " + _centreOfRotation.z);
		Debug.Log (GetComponent<Rigidbody2D>().transform.eulerAngles);
	}

	private void SnapToCentre(Collider2D collider){
		var pivotPosition = _centreOfRotation;
		var finalPosition = collider.gameObject.transform.position;
		if (finalPosition.Equals (pivotPosition)) {
			throw new ExecutionEngineException ("This only ever happens if you have duplicated nodes directly on top of each other.");
		}
		var remainingRotation = CalculateRemainingRotation (pivotPosition, finalPosition);
		RotateRigidbody2D (- Convert.ToSingle(remainingRotation), _centreOfRotation);
	}

	private void RotateRigidbody2D(float angle, Vector3 centre){
		try{
		GetComponent<Rigidbody2D> ().transform.RotateAround (centre, new Vector3 (0, 0, 1), angle);
		}
		catch(Exception e){
			Debug.Log (e);
		}
	}

	private double CalculateRemainingRotation(Vector3 pivotPosition, Vector3 finalPosition){
		var x = pivotPosition.x - finalPosition.x;
		var y = pivotPosition.y - finalPosition.y;
		var expectedAngleRads = Math.Atan (y / x);
		var expectedAngle = 180*expectedAngleRads / Math.PI;
		var currentAngle = GetComponent<Rigidbody2D> ().transform.eulerAngles.z;
		var remainingRotation1 = currentAngle - expectedAngle - 90;
		var remainingRotation2 = currentAngle - expectedAngle - 270;
		var remainingRotation = 0.0;
		if (Math.Abs (remainingRotation1) < Math.Abs (remainingRotation2)) {
			return remainingRotation1;
		} else {
			return remainingRotation2;
		}

	}
}