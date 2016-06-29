using System;
using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class NodeController : MonoBehaviour, INodeController
	{
		private Animator _animator;
		public Vector3 NodePosition {get{ return gameObject.transform.position; }}
		private int _disableHash = Animator.StringToHash ("Disable");
		private int _connectHash = Animator.StringToHash ("Connect");
		private int _reenableHash = Animator.StringToHash ("Reenable");
		public float TimeDisabledAfterDisconnect = 1.5f;
		public bool IsEnabled { get; private set; }

		public void Start(){
			_animator = GetComponent<Animator> ();
			IsEnabled = true;
		}

		public void Connect(){
			if (_animator != null) {
				_animator.SetTrigger (_connectHash);
			}
		}

		public void Disconnect(){
			if (_animator != null) {
				_animator.SetTrigger (_disableHash);
				IsEnabled = false;
				StartCoroutine("ReenableCoroutine");
			}
		}

		public IEnumerator ReenableCoroutine() {
			for(var i = 0;i < 1; i++) {
				yield return new WaitForSeconds(TimeDisabledAfterDisconnect);
			}
			for (var i = 0; i < 1; i++) {
				_animator.SetTrigger (_reenableHash);
				IsEnabled = true;
				yield return null;
			}
		}
	}
}

