using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BulletController : MonoBehaviour
	{
		public void OnBecameInvisible(){
			Destroy (gameObject);
		}

		public void Kill(){
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, 0);

			GetComponent<Animator> ().SetTrigger ("die");
		}
	}
}

