using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AssemblyCSharp
{
	public class BackButton : MonoBehaviour
	{
		FactoryOverlord _overlord;

		public void Start(){
			_overlord = GameObject.Find ("FactoryOverlord").GetComponent<FactoryOverlord>();
		}

		public void OnClick(){
			SceneManager.LoadScene (_overlord.PreviousScene);
		}
	}
}

