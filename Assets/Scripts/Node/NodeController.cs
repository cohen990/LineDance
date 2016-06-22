using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class NodeController : MonoBehaviour
	{
		private Animator _animator;
		public Vector3 NodePosition {get{ return gameObject.transform.position; }}
		private int _disableHash = Animator.StringToHash ("Disable");
		private int _connectHash = Animator.StringToHash ("Connect");

		public void Start(){
			_animator = GetComponent<Animator> ();
		}

		public void Connect(){
			if (_animator != null) {
				_animator.SetTrigger (_connectHash);
			}
		}

		public void Disconnect(){
			if (_animator != null) {
				_animator.SetTrigger (_disableHash);
			}
		}
	}
}

