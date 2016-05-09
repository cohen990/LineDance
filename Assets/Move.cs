using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var touch in Input.touches) {
			GetComponent<Rigidbody2D> ().rotation += 5;
		}
	}
}
