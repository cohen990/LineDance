using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class MouseControl : ControlBase
	{
		public MouseControl ()
		{
		}

		public override bool isTurningClockwise{
			get{
				return Input.GetMouseButton (1);
			}
		}

		public override bool isTurningCounterClockwise{
			get{
				return Input.GetMouseButton(0);
			}
		}
	}
}

