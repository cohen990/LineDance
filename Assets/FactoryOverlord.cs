using UnityEngine;
using System.Collections;

public class FactoryOverlord : MonoBehaviour {

	public string PreviousScene { get; private set; }
	public string NextLevel  { get; private set; }
	public string CurrentLevel  { get; private set; }

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
		PreviousScene = "lineMain";
	}

	public void SetPrevious (string sceneName)
	{
		PreviousScene = sceneName;
	}

	public void SetNextLevel (string levelName)
	{
		NextLevel = levelName;
	}

	public void SetCurrentLevel (string levelName){
		CurrentLevel = levelName;
	}

}
