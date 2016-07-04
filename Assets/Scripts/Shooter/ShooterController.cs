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
		private bool _disabled;


		public void Start(){
			StartCoroutine("ShootCoroutine");
			_disabled = false;
		}

		public IEnumerator ShootCoroutine(){
			while(!_disabled) {
				Shoot ();
				yield return new WaitForSeconds (BulletDelay);
			}
		}

		public void Shoot(){
			Rigidbody2D bulletClone = (Rigidbody2D) Instantiate(Bullet, transform.position, transform.rotation);
			bulletClone.velocity = GetDirectionOfShot() * BulletVelocity;
		}

		public Vector3 GetDirectionOfShot(){
			var aim = transform.Find ("Aim");

			var aimPos = aim.transform.position;
			var result =  aimPos - transform.position;

			return result;
		}

		public void Disable ()
		{
			_disabled = true;
		}
	}
}

