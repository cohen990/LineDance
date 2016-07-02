using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOverButton : MonoBehaviour {
	FactoryOverlord _overlord;

	public void Start(){
		_overlord = GameObject.Find ("FactoryOverlord").GetComponent<FactoryOverlord> ();
	}

	public void StartGame(){
		_overlord.SetPrevious ("lineMain");
		SceneManager.LoadScene("lineMain");
	}
}
