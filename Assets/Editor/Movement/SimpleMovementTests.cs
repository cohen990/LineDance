using NUnit.Framework;
using System;
using AssemblyCSharp.Movement;
using AssemblyCSharp;


namespace AssemblyCSharpEditor
{
	[TestFixture]
	public class SimpleMovementTests
	{
		[Test]
		public void GetVelocity_GoingClockwise_ReturnsTopSpeed(){
			var topSpeed = Convert.ToSingle(new Random().Next());

			var sut = new SimpleMovement (topSpeed);

			var result = sut.GetVelocity (Direction.Clockwise);

			Assert.That (result, Is.EqualTo (topSpeed));
		}

		[Test]
		public void GetVelocity_GoingCounterClockwise_ReturnsTopSpeed(){
			var topSpeed = Convert.ToSingle(new Random().Next());

			var sut = new SimpleMovement (topSpeed);

			var result = sut.GetVelocity (Direction.CounterClockwise);

			Assert.That (result, Is.EqualTo (topSpeed));
		}

		[Test]
		public void GetVelocity_GoingNoDirection_ReturnsTopSpeed(){
			var topSpeed = Convert.ToSingle(new Random().Next());

			var sut = new SimpleMovement (topSpeed);

			var result = sut.GetVelocity (Direction.NoDirection);

			Assert.That (result, Is.EqualTo (topSpeed));
		}
	}
}

