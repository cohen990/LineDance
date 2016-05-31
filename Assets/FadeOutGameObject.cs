using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class FadeOutGameObject : MonoBehaviour {

	private Image _image;
	private Stopwatch _watch;
	private bool _fadingOut = true;
	private CanvasGroup _group;

	// Use this for initialization
	void Start () {
		_image = GetComponent<Image> ();
		_watch = new Stopwatch ();
		_watch.Start ();
		_group = GetComponent<CanvasGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_fadingOut && _watch.ElapsedMilliseconds > 5000) {
			_group.alpha -= 0.1f;
		}
		if (_group.alpha <= 0) {
			_watch.Stop ();
			_watch.Reset ();
			_fadingOut = false;
		}
	}
}
