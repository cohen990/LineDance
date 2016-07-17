using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssemblyCSharp
{
	public class BackButton : MonoBehaviour
	{
		public string PreviousScene { get; set; }

		public void Start(){
		}

		public void OnClick(){
			SceneManager.LoadScene (PreviousScene);
		}
	}
}

