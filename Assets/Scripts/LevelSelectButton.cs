using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AssemblyCSharp
{
	public class LevelSelectButton : MonoBehaviour
	{
		public string LevelName;

		public void OnClick(){
			SceneManager.LoadScene(LevelName);
		}
	}
}

