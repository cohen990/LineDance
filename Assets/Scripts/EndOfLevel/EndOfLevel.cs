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

		public EndOfLevel(){
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
				return () => SceneManager.LoadScene (LevelNames.Next());
			}

			return () => {
				return;
			};
		}
	}
}

