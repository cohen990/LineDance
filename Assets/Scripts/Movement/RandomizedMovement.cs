using System;
using UnityEngine;

namespace AssemblyCSharp.Movement
{
	public class RandomizedMovement : MovementBase
	{
		public RandomizedMovement (Rigidbody2D subject, float topSpeed) : base(subject, topSpeed)
		{
		}

		public override float GetVelocity(Direction direction){
			return Convert.ToSingle( new System.Random().NextDouble() * _topSpeed*2);
		}

		public override void AlertOfHitNode(){
			/* no op */
		}

		public override void AlertOfStoppedMoving(){
			/* no op */
		}
	}
}

