﻿using System;
using UnityEngine;
using System.Linq;

namespace AssemblyCSharp
{
	public class TouchControl : ControlBase
	{
		public override bool isTurningClockwise {
			get{
				var input = Input.touches.LastOrDefault ();
				if (!input.Equals(default(Touch)))
				{
					if(input.position.x > Screen.width/2)
					{
						return true;
					}
				}

				return false;
			}
		}

		public override bool isTurningCounterClockwise {
			get{ 
				var input = Input.touches.LastOrDefault ();
				if (!input.Equals(default(Touch)))
				{
					if(input.position.x < Screen.width/2)
					{
						return true;
					}
				}

				return false;
			}
		}
	}
}

