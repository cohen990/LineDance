using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;
using AssemblyCSharp;

namespace AssemblyCSharp
{
	public class FadeOutGameObject : MonoBehaviour {

		private Image _image;
		private Stopwatch _watch;
		private bool _fadingOut = true;

		// Use this for initialization
		void Start () {
			_image = GetComponent<Image> ();
			_watch = new Stopwatch ();
			_watch.Start ();
		}

		// Update is called once per frame
		void Update () {
			if (_fadingOut && _watch.ElapsedMilliseconds > 2000) {
				_image.CrossFadeAlpha (0f, 1.5f, false);
				_fadingOut = false;
				_watch.Stop ();
				_watch.Reset ();
			}
		}
	}
}