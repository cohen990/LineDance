using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
	FactoryOverlord _overlord;

	public void Start(){
		_overlord = GameObject.Find ("FactoryOverlord").GetComponent<FactoryOverlord> ();
	}

	public void StartGame(){
		_overlord.SetPrevious (SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("World-select");
	}
}
