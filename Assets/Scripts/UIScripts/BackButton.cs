using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssemblyCSharp
{
	public class BackButton : MonoBehaviour
	{
		public string Previous;
		public void OnClick(){
			SceneManager.LoadScene (Previous);
		}
	}
}

