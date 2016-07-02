using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AssemblyCSharp
{
	public class LevelSelectButton : MonoBehaviour
	{
		public string LevelName;

		FactoryOverlord _overlord;

		public void Start(){
			_overlord = GameObject.Find ("FactoryOverlord").GetComponent<FactoryOverlord> ();
		}

		public void OnClick(){
			_overlord.SetCurrentLevel (LevelName);
			_overlord.SetPrevious (SceneManager.GetActiveScene().name);
			SceneManager.LoadScene(LevelName);
		}
	}
}

