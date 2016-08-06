using System;
using UnityEngine;

namespace AssemblyCSharp.Movement
{
	public class SimpleMovement : MovementBase
	{
		public SimpleMovement (GameObject subject, float topSpeed) : base(subject, topSpeed)
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

