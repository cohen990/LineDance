using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

namespace AssemblyCSharp
{
	public class EndOfLevel : IEndOfLevel
	{
		private bool _hasEnded = false;
		private Stopwatch _watch = new Stopwatch();
		private FactoryOverlord _overlord;

		public EndOfLevel(){
			_overlord = GameObject.Find ("FactoryOverlord").GetComponent<FactoryOverlord> ();
		}

		public void AlertOfEnd(){
			_hasEnded = true;
			_watch.Stop ();
			_watch.Reset ();
			_watch.Start ();
		}

		public bool IsEndOfLevel(){
			return _hasEnded;
		}

		public Action GetAction(){
			// This badly needs changing - need to use a coroutine instead - and put it on the overlord.
			#error "this code is shit"
			if (_watch.ElapsedMilliseconds > 2000) {
				return () => SceneManager.LoadScene (_overlord.MoveNextLevel());
			}

			return () => {
				return;
			};
		}
	}
}

