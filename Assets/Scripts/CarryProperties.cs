using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;
using AssemblyCSharp.Movement;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class CarryProperties
{
	public Vector3 Centre;
	public float Speed;
	public Direction Direction;

	public CarryProperties(Vector3 centre, float speed, Direction direction){
		Centre = centre;
		Speed = speed;
		Direction = direction;
	}
}
