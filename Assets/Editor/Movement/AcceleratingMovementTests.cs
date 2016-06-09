using NUnit.Framework;
using System;
using AssemblyCSharp.Movement;
using AssemblyCSharp;


namespace AssemblyCSharpEditor
{
	[TestFixture]
	public class AcceleratingMovementTests
	{
		[Test]
		public void GetVelocity_NewInstance_ReturnsAcceleration(){
			var topSpeed = int.MaxValue;
			var acceleration = Convert.ToSingle(new Random().NextDouble());

			var sut = new AcceleratingMovement (topSpeed, acceleration);

			var result = sut.GetVelocity (Direction.Clockwise);

			Assert.That (result, Is.EqualTo (acceleration));
		}

		[Test]
		public void GetVelocity_CalledTwice_ReturnsTwiceAcceleration(){
			var topSpeed = int.MaxValue;
			var acceleration = Convert.ToSingle(new Random().NextDouble());

			var sut = new AcceleratingMovement (topSpeed, acceleration);

			sut.GetVelocity (Direction.Clockwise);
			var result = sut.GetVelocity (Direction.Clockwise);

			Assert.That (result, Is.EqualTo (2 * acceleration));
		}

		[Test]
		public void GetVelocity_DoesntExceedTopSpeedTimesTwo(){
			var topSpeed = 0.8F;
			var acceleration = 0.7F;

			var sut = new AcceleratingMovement (topSpeed, acceleration);

			sut.GetVelocity (Direction.Clockwise);
			var result = sut.GetVelocity (Direction.Clockwise);

			Assert.That (result, Is.Not.GreaterThan(topSpeed  * 2));
		}

		[Test]
		public void GetVelocity_ChangingDirection_ResetsCurrentSpeed(){
			var topSpeed = int.MaxValue;
			var acceleration = 0.1F;

			var sut = new AcceleratingMovement (topSpeed, acceleration);

			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			var result = sut.GetVelocity (Direction.CounterClockwise);

			Assert.That (result, Is.EqualTo(acceleration));
		}

		[Test]
		public void GetVelocity_HitNode_ResetsCurrentSpeed(){
			var topSpeed = int.MaxValue;
			var acceleration = 0.1F;

			var sut = new AcceleratingMovement (topSpeed, acceleration);

			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.AlertOfHitNode ();
			var result = sut.GetVelocity (Direction.Clockwise);

			Assert.That (result, Is.EqualTo(acceleration));
		}

		[Test]
		public void GetVelocity_StopsMoving_ResetsCurrentSpeed(){
			var topSpeed = int.MaxValue;
			var acceleration = 0.1F;

			var sut = new AcceleratingMovement (topSpeed, acceleration);

			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.GetVelocity (Direction.Clockwise);
			sut.AlertOfStoppedMoving ();
			var result = sut.GetVelocity (Direction.Clockwise);

			Assert.That (result, Is.EqualTo(acceleration));
		}
	}
}

