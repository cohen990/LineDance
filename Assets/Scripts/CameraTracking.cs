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

			Zoom ();
		}

		void Zoom(){

			// If there are two touches on the device...
			if (Input.touchCount == 2)
			{
				var camera = GetComponent<Camera> ();
				var orthoZoomSpeed = 0.1f;
				var perspectiveZoomSpeed = 0.1f;
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				// If the camera is orthographic...
				if (camera.orthographic)
				{
					// ... change the orthographic size based on the change in distance between the touches.
					camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

					// Make sure the orthographic size never drops below zero.
					camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
				}
				else
				{
					// Otherwise change the field of view based on the change in distance between the touches.
					camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

					// Clamp the field of view to make sure it's between 0 and 180.
					camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
				}
			}
		}
			
	}
}

