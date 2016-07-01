using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AssemblyCSharp
{
	public class LevelSelectButton : MonoBehaviour
	{
		public string LevelName;
		public string NextLevelName;

		FactoryOverlord _overlord;

		public void Start(){
			_overlord = GameObject.Find ("FactoryOverlord").GetComponent<FactoryOverlord> ();
		}

		public void OnClick(){
			_overlord.SetNextLevel (NextLevelName);
			_overlord.SetCurrentLevel (LevelName);
			SceneManager.LoadScene(LevelName);
		}
	}
}

