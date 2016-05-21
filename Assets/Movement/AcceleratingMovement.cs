using System;

namespace AssemblyCSharp.Movement
{
	public class AcceleratingMovement : MovementBase
	{
		private float _acceleration = 0.2F;

		private float _currentSpeed;

		private Direction _lastKnownDirection;

		public AcceleratingMovement (float topSpeed) : base(topSpeed * 2)
		{
			_currentSpeed = 0;
			_lastKnownDirection = Direction.Motionless;
		}

		public override float GetVelocity(Direction direction){
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

