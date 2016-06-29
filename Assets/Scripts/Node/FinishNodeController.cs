using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class FinishNodeController : MonoBehaviour, INodeController
	{
		private Animator _animator;
		public bool IsEnabled { get { return true; } }

		public void Connect ()
		{
			GetComponent<ParticleSystem> ().Play ();
		}

		public void Disconnect ()
		{
			throw new NotImplementedException ();
		}

		public Vector3 NodePosition { get { return transform.position; } }


		public void Start(){
			_animator = GetComponent<Animator> ();
		}
	}
}

