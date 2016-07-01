using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;
using AssemblyCSharp.Movement;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	private ControlBase _controls;
	private Vector3 _centreOfRotation;
	private MovementBase _movement;
	private Direction _currentDirection;
	private IEndOfLevel _endOfLevel;
	private bool _isDead;
	private Stopwatch _afterDeathStopwatch;
	private int _waitAfterDeath = 2000;
	public ControlType ControlType;
	private bool _isBeingCarried = false;
	private Rigidbody2D _rigidBody;
	private Vector3 _previousCarrierPosition;
	internal INodeController ConnectedNodeController;

	public Player(){
	}

	// Use this for initialization
	void Start () {
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		if (ControlType == ControlType.Mouse) {
			_controls = new MouseControl ();
		} else if (ControlType == ControlType.Touch) {
			_controls = new TouchControl ();
		}
		_movement = new AcceleratingMovement (_rigidBody, 4);
		_centreOfRotation = _rigidBody.transform.position;
		_endOfLevel = new EndOfLevel ();
		_isDead = false;
		_movement.AlertOfStartOfLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_isDead) {
			if (_afterDeathStopwatch.ElapsedMilliseconds > _waitAfterDeath) {
				SceneManager.LoadScene ("LevelSelectGreen");
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
		if (_isBeingCarried) {
			DoCarryMotion ();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag.ToLower () == "bullet") {
			DoBulletTrigger (col);
		}
		if (col.gameObject.tag.ToLower () == "barrier") {
			DoBarrierTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "finish") {
			DoFinishTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "enemy") {
			DoEnemyTrigger (col);
		} else if (col.gameObject.tag.ToLower () == "movingnode") {
			DoMovingNodeTrigger (col);
		}
		else if (col.gameObject.tag.ToLower () == "node") {
			DoNodeTrigger (col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag.ToLower () == "barrier") {
			_movement.AlertOfLeavingBarrier ();
		}
	}

	void DoBulletTrigger (Collider2D col)
	{
		Die ();
	}

	void DoEnemyTrigger (Collider2D col)
	{
		Die ();
	}

	void DoFinishTrigger(Collider2D col){
		_endOfLevel.AlertOfEnd ();
		DoNodeTrigger (col.gameObject);
	}

	private void DoNodeTrigger(GameObject node){
		var newNodeController = node.GetComponent<INodeController> ();
		if (newNodeController.IsEnabled) {

			_movement.AlertOfHitNode ();
			_centreOfRotation = _movement.SnapToCentre (node.transform.position, _centreOfRotation);

			if (ConnectedNodeController != null) {
				ConnectedNodeController.Disconnect ();
			}
			ConnectedNodeController = newNodeController;

			ConnectedNodeController.Connect ();
			_isBeingCarried = false;
		}
	}

	void DoBarrierTrigger(Collider2D col){
		_movement.AlertOfHitBarrier (_currentDirection);
	}

	void DoMovingNodeTrigger (Collider2D col)
	{
		var moveScript = col.gameObject.GetComponent<MoveInCircle> ();
		DoNodeTrigger (col.gameObject);
		_isBeingCarried = true;
		_previousCarrierPosition = col.gameObject.transform.position;
	}

	void DoCarryMotion ()
	{
		if (!_previousCarrierPosition.Equals (ConnectedNodeController.NodePosition)) {
			var difference = ConnectedNodeController.NodePosition - _previousCarrierPosition;
			_rigidBody.transform.position += difference;
			_centreOfRotation = ConnectedNodeController.NodePosition;
		}
		_previousCarrierPosition = ConnectedNodeController.NodePosition;
	}

	void Die ()
	{
		if (!_isDead) {
			var animator = GetComponent<Animator> ();
			animator.Play ("Die");
			_isDead = true;
			_movement.AlertOfDead ();
			_afterDeathStopwatch = Stopwatch.StartNew ();
		}
	}
}