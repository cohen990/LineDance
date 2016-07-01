using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class ShooterController : MonoBehaviour
	{
		public Rigidbody2D Bullet;
		public int BulletVelocity;
		public float BulletDelay;

		public void Start(){
			StartCoroutine("ShootCoroutine");
		}

		public IEnumerator ShootCoroutine(){
			for (;;) {
				Shoot ();
				yield return new WaitForSeconds (BulletDelay);
			}
		}

		public void Shoot(){
			Rigidbody2D bulletClone = (Rigidbody2D) Instantiate(Bullet, transform.position, transform.rotation);
			bulletClone.velocity = GetDirectionOfShot() * BulletVelocity;

			Debug.Log ("bang");
		}

		public Vector3 GetDirectionOfShot(){
			var aim = transform.Find ("Aim");

			var aimPos = aim.transform.position;
			var result =  aimPos - transform.position;

			return result;
		}
	}
}

