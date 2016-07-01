using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BulletController : MonoBehaviour
	{
		public void OnBecameInvisible(){
			Destroy (gameObject);
		}
	}
}

