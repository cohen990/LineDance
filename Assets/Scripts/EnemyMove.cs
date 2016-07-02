using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;
using AssemblyCSharp.Movement;

public class EnemyMove : MonoBehaviour {
	private Vector3 _centreOfRotation;
	private MovementBase _movement;
	private Direction _currentDirection;
	public Direction DefaultDirection;
	private bool _isDead;

	// Use this for initialization
	void Start () {
		_movement = new AcceleratingMovement (GetComponent<Rigidbody2D>(), 4);
		_centreOfRotation = GetComponent<Rigidbody2D> ().transform.position;
		_currentDirection = DefaultDirection;
		_isDead = false;
	}

	// Update is called once per frame
	void Update () {
		if (!_isDead) {
			if (_movement.IsBouncing ()) {
				_movement.Bounce (_centreOfRotation);
			}
			_movement.Turn (_currentDirection, _centreOfRotation);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag.ToLower () == "node") {
			DoNodeTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "barrier") {
			DoBarrierTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "finish") {
			DoNodeTrigger (col);
		}
	}

	void DoNodeTrigger(Collider2D col){
		_movement.AlertOfHitNode ();
		_movement.SnapToCentre (col.gameObject.transform.position, _centreOfRotation);
		_centreOfRotation = col.gameObject.transform.position;
	}

	void DoBarrierTrigger(Collider2D col){
		_movement.AlertOfHitBarrier (_currentDirection);
		ReverseDirection ();
	}

	void ReverseDirection(){
		if (_currentDirection == Direction.Clockwise) {
			_currentDirection = Direction.CounterClockwise;
		}
		else if (_currentDirection == Direction.CounterClockwise) {
			_currentDirection = Direction.Clockwise;
		}
	}

	public void Kill ()
	{
		var animator = GetComponent<Animator> ();
		animator.Play ("Die");
		_isDead = true;
	}
}