using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class TouchControl : ControlBase
	{
		public override bool isTurningClockwise {
			get{ 
				return (Input.touches.Length == 1);
			}
		}

		public override bool isTurningCounterClockwise {
			get{ 
				return (Input.touches.Length == 2);
			}
		}
	}
}

