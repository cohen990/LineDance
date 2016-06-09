using System;

namespace AssemblyCSharp.Movement
{
	public class AcceleratingMovement : MovementBase
	{
		private float _acceleration;

		private Direction _lastKnownDirection;

		private bool _collided;

		public AcceleratingMovement (float topSpeed, float acceleration = 0.2F) : base(topSpeed * 2)
		{
			_currentSpeed = 0;
			_lastKnownDirection = Direction.NoDirection;
			_collided = false;
			_acceleration = acceleration;
			_bounceDeceleration = _acceleration * 2;
		}

		public override float GetVelocity(Direction direction){
			if (!_canMove || _collided) {
				return 0;
			}

			if (direction.Equals (_directionOfBarrier)) {
				return 0;
			}

			if (_lastKnownDirection != direction) {
				_currentSpeed = 0;
				_lastKnownDirection = direction;
			}

			if (_currentSpeed < _topSpeed) {
				_currentSpeed += _acceleration;
			}

			return _currentSpeed;
		}

		public override void AlertOfHitNode(){
			_currentSpeed = 0;
		}

		public override void AlertOfStoppedMoving(){
			_currentSpeed = 0;
		}
	}
}

