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
		private string _nextLevel;

		public EndOfLevel(string nextLevel){
			_nextLevel = nextLevel;
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
			if (_watch.ElapsedMilliseconds > 2000) {
				return () => SceneManager.LoadScene (_nextLevel);
			}

			return () => {
				return;
			};
		}
	}
}

