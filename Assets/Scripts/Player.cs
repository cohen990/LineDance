using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;
using AssemblyCSharp.Movement;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	private ControlBase _controls = new MouseControl();
	private Vector3 _centreOfRotation;
	private MovementBase _movement;
	private Direction _currentDirection;
	private IEndOfLevel _endOfLevel;
	private bool _isDead;
	private Stopwatch _afterDeathStopwatch;
	private int _waitAfterDeath = 2000;

	public Player(){
	}

	// Use this for initialization
	void Start () {
		_movement = new AcceleratingMovement (GetComponent<Rigidbody2D>(), 4);
		_centreOfRotation = GetComponent<Rigidbody2D> ().transform.position;
		_endOfLevel = new EndOfLevel ();
		_isDead = false;
		_movement.AlertOfStartOfLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_isDead) {
			if (_afterDeathStopwatch.ElapsedMilliseconds > _waitAfterDeath) {
				SceneManager.LoadScene (LevelNames.Current ());
			}
		}
		if (_endOfLevel.IsEndOfLevel ()) {
			_endOfLevel.GetAction ().Invoke ();
			return;
		}
		if (_movement.IsBouncing ()) {
			_movement.Bounce (_centreOfRotation);
		} else {
			_currentDirection = _controls.turningDirection;
			_movement.Turn (_currentDirection, _centreOfRotation);
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
			UnityEngine.Debug.Log ("die");
			_isDead = true;
			_movement.AlertOfDead ();
			_afterDeathStopwatch = Stopwatch.StartNew ();
		}
	}

	void DoFinishTrigger(Collider2D col){
		col.gameObject.GetComponent<ParticleSystem> ().Play ();
		_endOfLevel.AlertOfEnd ();
		DoNodeTrigger (col);
	}

	void DoNodeTrigger(Collider2D col){
		_movement.AlertOfHitNode ();
		_movement.SnapToCentre (col.gameObject.transform.position, _centreOfRotation);
		_centreOfRotation = col.gameObject.transform.position;
		var animator = col.gameObject.GetComponent<Animator> ();
		animator.Play ("Connect");
	}

	void DoBarrierTrigger(Collider2D col){
		_movement.AlertOfHitBarrier (_currentDirection);
	}
}