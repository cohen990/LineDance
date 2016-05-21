using System;

namespace AssemblyCSharp.Movement
{
	public class RandomizedMovement : MovementBase
	{
		public RandomizedMovement (float topSpeed) : base(topSpeed)
		{
		}

		public override float GetVelocity(Direction direction){
			return Convert.ToSingle( new Random().NextDouble() * _topSpeed*2);
		}

		public override void AlertOfHitNode(){
			/* no op */
		}

		public override void AlertOfStoppedMoving(){
			/* no op */
		}
	}
}

