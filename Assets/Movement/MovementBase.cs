using System;

namespace AssemblyCSharp.Movement
{
	public abstract class MovementBase
	{
		protected float _topSpeed;

		public MovementBase(float topSpeed){
			_topSpeed = topSpeed;
		}

		public abstract float GetVelocity (Direction direction);

		public abstract void AlertOfHitNode ();

		public abstract void AlertOfStoppedMoving();
	}
}

