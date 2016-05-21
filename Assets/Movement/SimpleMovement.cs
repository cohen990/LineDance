using System;

namespace AssemblyCSharp.Movement
{
	public class SimpleMovement : MovementBase
	{
		public SimpleMovement (float topSpeed) : base(topSpeed)
		{
		}

		public override float GetVelocity(Direction direction){
			return _topSpeed;
		}

		public override void AlertOfHitNode(){
			/* no op */
		}

		public override void AlertOfStoppedMoving(){
			/* no op */
		}
	}
}

