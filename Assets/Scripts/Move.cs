﻿using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;
using AssemblyCSharp.Movement;

public class Move : MonoBehaviour {
	private ControlBase _controls = new MouseControl();
	private Vector3 _centreOfRotation;
	private MovementBase _movement;
	private Direction _currentDirection;
	private IEndOfLevel _endOfLevel;
	private bool _isDead;

	public Move(){
		_movement = new AcceleratingMovement (4);
	}

	// Use this for initialization
	void Start () {
		_centreOfRotation = GetComponent<Rigidbody2D> ().transform.position;
		_endOfLevel = new EndOfLevel (LevelNames.Next());
		_isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_endOfLevel.IsEndOfLevel()) {
			_endOfLevel.GetAction ().Invoke ();
			return;
		}
		if (_movement.IsBouncing()) {
			RotateRigidbody2D (_movement.GetBounceVelocity (), _centreOfRotation);
		}
		else if (_controls.isTurningCounterClockwise) {
  			_currentDirection = Direction.CounterClockwise;
			RotateRigidbody2D (_movement.GetVelocity (Direction.CounterClockwise), _centreOfRotation);
		} else if (_controls.isTurningClockwise) {
			_currentDirection = Direction.Clockwise;
			RotateRigidbody2D (-_movement.GetVelocity (Direction.Clockwise), _centreOfRotation);
		} else {
			_movement.AlertOfStoppedMoving();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag.ToLower () == "node") {
			DoNodeTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "barrier") {
			DoBarrierTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "finish") {
			DoFinishTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "enemy") {
			DoEnemyTrigger (col);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag.ToLower () == "barrier") {
			_movement.AlertOfLeavingBarrier ();
		}
	}

	void DoEnemyTrigger (Collider2D col)
	{
		if (!_isDead) {
			var animator = GetComponent<Animator> ();
			animator.Play ("Die");
			Debug.Log ("die");
			_isDead = true;
		}
	}

	void DoFinishTrigger(Collider2D col){
		col.gameObject.GetComponent<ParticleSystem> ().Play ();
		_endOfLevel.AlertOfEnd ();
		DoNodeTrigger (col);
	}

	void DoNodeTrigger(Collider2D col){
		_movement.AlertOfHitNode ();
		SnapToCentre (col);
		_centreOfRotation = col.gameObject.transform.position;
	}

	void DoBarrierTrigger(Collider2D col){
		Debug.Log ("hit a barrier");
		_movement.AlertOfHitBarrier (_currentDirection);
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
		var deltaX = pivotPosition.x - finalPosition.x;
		var deltaY = pivotPosition.y - finalPosition.y;
		var expectedAngleRads = Math.Atan (deltaY / deltaX);
		var expectedAngle = 180*expectedAngleRads / Math.PI;
		var currentAngle = GetComponent<Rigidbody2D> ().transform.eulerAngles.z;
		var remainingRotation = currentAngle - expectedAngle;
		var modded = remainingRotation % 90;
		var lowestFound = modded;
		if (Math.Abs (lowestFound - 90) < Math.Abs (lowestFound)) {
			lowestFound -= 90;
		}
		if (Math.Abs (lowestFound + 90) < Math.Abs (lowestFound)) {
			lowestFound += 90;
		}
		if (Math.Abs(lowestFound) >50) {
			throw new ExecutionEngineException ("This should never happen! It's that bug in the snap to bullshit again");
		}
		return lowestFound;
	}
}