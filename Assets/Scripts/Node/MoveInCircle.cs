using System;
using UnityEngine;
using AssemblyCSharp.Movement;

namespace AssemblyCSharp
{
	public class MoveInCircle : MonoBehaviour
	{
		public float Speed;
		public Vector3 Centre;
		private GameObject _connectedObject;
		public Direction Direction;

		public void Update(){
			if (Direction == Direction.Clockwise) {
				gameObject.transform.RotateAround (Centre, new Vector3 (0, 0, 1), -Speed);
			}
			gameObject.transform.RotateAround (Centre, new Vector3 (0, 0, 1), Speed);
		}
	}
}

