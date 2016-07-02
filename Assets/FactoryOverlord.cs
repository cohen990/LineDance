using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FactoryOverlord : MonoBehaviour {

	public string PreviousScene { get; private set; }
	public string CurrentScene  { get; private set; }

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
		PreviousScene = "lineMain";
	}

	public void SetPrevious (string sceneName)
	{
		PreviousScene = sceneName;
	}

	public string MoveNextLevel ()
	{
		var next = Levels.Get [CurrentScene] ?? PreviousScene;
		CurrentScene = next;
		return next;
	}

	public void SetCurrentLevel (string levelName){
		CurrentScene = levelName;
	}
}