using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
	public void StartGame(){
		SceneManager.LoadScene("World-select");
	}
}
