using System;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

namespace AssemblyCSharp
{
	public class FadeOutText : MonoBehaviour
	{
		private Text _text;
		private Stopwatch _watch;
		private bool _fadingOut = true;

		// Use this for initialization
		void Start () {
			_text = GetComponent<Text> ();
			_watch = new Stopwatch ();
			_watch.Start ();
		}

		// Update is called once per frame
		void Update () {
			if (_fadingOut && _watch.ElapsedMilliseconds > 2000) {
 					var color = _text.color;
				var newA = color.a -= 0.025f;
				var newColor = new Color (color.r, color.g, color.b, newA);
				_text.color = newColor;
				_watch.Stop ();
			}
			if (_text.color.a <= 0) {
				_fadingOut = false;
				_watch.Reset ();
			}
		}
	}
}

