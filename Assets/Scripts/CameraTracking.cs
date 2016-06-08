using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class CameraTracking : MonoBehaviour
	{
		void Start(){
			var playerPosition = GameObject.FindWithTag ("Player").transform.position;
			var newPos = new Vector3 (playerPosition.x, playerPosition.y, transform.position.z);
			transform.position = newPos;
		}

		void Update(){
			var playerPosition = GameObject.FindWithTag ("Player").transform.position;
			var newPos = new Vector3 (playerPosition.x, playerPosition.y, transform.position.z);
			transform.position = newPos;
		}
			
	}
}

