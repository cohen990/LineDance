using System;

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

		public MovementBase(float topSpeed){
			_topSpeed = topSpeed;
			_bounceDeceleration = 0.4F;
			_currentSpeed = topSpeed;
			_isBouncing = false;
		}

		public abstract float GetVelocity (Direction direction);

		public abstract void AlertOfHitNode ();

		public abstract void AlertOfStoppedMoving ();

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
	}
}

