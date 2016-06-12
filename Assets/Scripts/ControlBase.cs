using System;

namespace AssemblyCSharp
{
	public abstract class ControlBase
	{
		public abstract bool isTurningClockwise { get; }
		public abstract bool isTurningCounterClockwise { get; }


		public virtual Direction turningDirection {
			get {
				if (isTurningClockwise) {
					return Direction.Clockwise;
				} else if (isTurningCounterClockwise) {
					return Direction.CounterClockwise;
				}

				return Direction.NoDirection;
			}
		}
	}
}

