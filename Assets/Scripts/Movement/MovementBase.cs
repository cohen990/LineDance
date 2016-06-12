using System;
using UnityEngine;

namespace AssemblyCSharp.Movement
{
	public abstract class MovementBase
	{
		protected float _topSpeed;
		protected float _currentSpeed;
		protected float _bounceDeceleration;
		private Direction _bounceDirection;
		private bool _isBouncing;
		protected Direction _directionOfBarrier;
		protected bool _canMove;
		protected Rigidbody2D _target;

		public MovementBase(Rigidbody2D target, float topSpeed){
			_topSpeed = topSpeed;
			_bounceDeceleration = 0.4F;
			_currentSpeed = topSpeed;
			_isBouncing = false;
			_canMove = true;
			_target = target;
		}

		public abstract float GetVelocity (Direction direction);

		public abstract void AlertOfHitNode ();

		public abstract void AlertOfStoppedMoving ();

		public virtual void AlertOfStartOfLevel (){
			_canMove = true;
		}

		public virtual void AlertOfDead (){
			_canMove = false;
		}

		public virtual void AlertOfHitBarrier (Direction direction){
			_isBouncing = true;
			if (direction.Equals(Direction.Clockwise)) {
				_bounceDirection = Direction.CounterClockwise;
			} else {
				_bounceDirection = Direction.Clockwise;
			}
			_directionOfBarrier = direction;
		}

		public float GetBounceVelocity(){
			_currentSpeed -= _bounceDeceleration;
			if (_bounceDirection.Equals (Direction.CounterClockwise)) {
				return _currentSpeed;
			} else {
				return -_currentSpeed;
			}
		}

		public bool IsBouncing(){
			if (!_isBouncing) {
				return false;
			}
			if (_currentSpeed <= 0) {
				_isBouncing = false;
				return false;
			}
			return true;
		}

		public void AlertOfLeavingBarrier ()
		{
			_directionOfBarrier = Direction.NoDirection;
		}

		public void Bounce(Vector3 centreOfRotation){
			RotateRigidbody2D (GetBounceVelocity (), centreOfRotation);
		}

		private void RotateRigidbody2D(float angle, Vector3 centre){
			try{
				_target.transform.RotateAround (centre, new Vector3 (0, 0, 1), angle);
			}
			catch(Exception e){
				UnityEngine.Debug.Log (e);
			}
		}

		public void Turn (Direction turningDirection, Vector3 centreOfRotation)
		{
			if (turningDirection == Direction.CounterClockwise) {
				RotateRigidbody2D (GetVelocity (Direction.CounterClockwise), centreOfRotation);
			} else if (turningDirection == Direction.Clockwise) {
				RotateRigidbody2D (-GetVelocity (Direction.Clockwise), centreOfRotation);
			} else {
				AlertOfStoppedMoving();
			}
		}

		public void SnapToCentre(Vector3 position, Vector3 centreOfRotation){
			var pivotPosition = centreOfRotation;
			var finalPosition = position;
			if (finalPosition.Equals (pivotPosition)) {
				throw new ExecutionEngineException ("This only ever happens if you have duplicated nodes directly on top of each other.");
			}
			var remainingRotation = CalculateRemainingRotation (pivotPosition, finalPosition);

			RotateRigidbody2D (- Convert.ToSingle(remainingRotation), centreOfRotation);
		}



		private double CalculateRemainingRotation(Vector3 pivotPosition, Vector3 finalPosition){
			var deltaX = pivotPosition.x - finalPosition.x;
			var deltaY = pivotPosition.y - finalPosition.y;
			var expectedAngleRads = Math.Atan (deltaY / deltaX);
			var expectedAngle = 180*expectedAngleRads / Math.PI;
			var currentAngle = _target.transform.eulerAngles.z;
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
}

