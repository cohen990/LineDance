using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;
using AssemblyCSharp.Movement;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour {
	private ControlBase _controls;
	private Vector3 _centreOfRotation;
	private MovementBase _movement;
	private Direction _currentDirection;
	private bool _isDead;
	private Stopwatch _afterDeathStopwatch;
	private bool _isBeingCarried = false;
	private Rigidbody2D _rigidBody;
	private Vector3 _previousCarrierPosition;
	private bool _isEndOfLevel;
	internal INodeController ConnectedNodeController;
	private Vector3[] _previousFramePointsAlongLength;

	public Player(){
	}

	FactoryOverlord _overlord;

	// Use this for initialization
	void Start () {
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		if(_isTouchDevice()){
			_controls = new TouchControl ();
		} else {
			_controls = new MouseControl ();
		}
		_movement = new AcceleratingMovement (_rigidBody, 4);
		_centreOfRotation = _rigidBody.transform.position;
		_isDead = false;
		_isEndOfLevel = false;
		_movement.AlertOfStartOfLevel ();
		try{
			_overlord = GameObject.Find ("FactoryOverlord").GetComponent<FactoryOverlord> ();
		}
		catch(Exception){
			UnityEngine.Debug.Log("No overlord was found. Hopefully you are testing ;)");
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (_isDead) {
			_overlord.PlayerDie ();
		}
		if (_isEndOfLevel) {
			_overlord.MoveNextLevel ();
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

		var currentFramePointsAlongLength = GetPointsAlongLength ();

		if (CheckForMissedCollisions (currentFramePointsAlongLength)) {
			Debug.Log ("missed collision");
		}

		_previousFramePointsAlongLength = currentFramePointsAlongLength;
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
		_isEndOfLevel = true;
		_overlord.FinishLevel ();
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

	bool _isTouchDevice ()
	{
		if (Application.platform == RuntimePlatform.Android) {
			return true;
		}
		return false;
	}

	Vector3[] GetPointsAlongLength(){
		var centre = gameObject.transform.position;
		var collider = gameObject.GetComponent<BoxCollider2D> ();
		var size = collider.bounds;
		var rotation = gameObject.transform.eulerAngles;

		var firstPointX = size.center.x + size.extents.x;
		var firstPointY = size.center.y + size.extents.y;
		var lastPointX = size.center.x - size.extents.x;
		var lastPointY = size.center.y - size.extents.y;

		if (rotation.z < 90 || rotation.z < 270) {
			firstPointX = size.center.x - size.extents.x;
			lastPointX = size.center.x + size.extents.x;
		}
		if (rotation.z >= 90 && rotation.z < 180) {
			firstPointY = size.center.y - size.extents.y;
			lastPointY = size.center.y + size.extents.y;
		}

		var firstPoint = new Vector3 (firstPointX, firstPointY);

		var lastPoint = new Vector3 (lastPointX, lastPointY);

		var points = GetPointsBetweenPoints (firstPoint, lastPoint);

		return points;
	}

	Vector3[] GetPointsBetweenPoints(Vector3 beginning, Vector3 end, int numberOfPoints = 20){
		var arr = new Vector3[numberOfPoints + 1];
		arr [0] = beginning;
		arr [numberOfPoints] = end;

		var xDistance = beginning.x - end.x;
		var yDistance = beginning.y - end.y;
		var xInterval = xDistance / numberOfPoints;
		var yInterval = yDistance / numberOfPoints;

		for(var i = 1; i < numberOfPoints;i++){
			arr [i] = beginning - new Vector3 (xInterval * i, yInterval * i);
		}

		return arr;
	}

	bool CheckForMissedCollisions (Vector3[] points)
	{
		if (_previousFramePointsAlongLength == null || points == null) {
			return false;
		}

		for (var i = 0; i < points.Length; i++) {
			Debug.DrawLine(points[i], _previousFramePointsAlongLength[i]);
			var hit = Physics2D.Raycast (points [i], _previousFramePointsAlongLength [i]);
			if (hit.collider != null) {
				if (hit.collider.gameObject.tag.ToLower() == "node" && hit.collider.gameObject.transform.position != ConnectedNodeController.NodePosition) {
					var node = hit.collider.gameObject;

					var controller = node.GetComponent<INodeController> ();

					if (controller.IsEnabled) {
						return true;
					}
				}
			}
		}

		return false;
	}
}