using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOverButton : MonoBehaviour {
	public void StartGame(){
		SceneManager.LoadScene("lineMain");
	}
}
