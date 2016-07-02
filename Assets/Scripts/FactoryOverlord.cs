using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.SceneManagement;

public class FactoryOverlord : MonoBehaviour {

	public string PreviousScene { get; private set; }
	public string CurrentScene  { get; private set; }
	private bool _isEndOfLevel;

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
		PreviousScene = "lineMain";
		CurrentScene = SceneManager.GetActiveScene().name;
		_isEndOfLevel = false;
	}

	public void SetPrevious (string sceneName)
	{
		PreviousScene = sceneName;
	}

	public void MoveNextLevel ()
	{
		if (!_isEndOfLevel) {
			var next = Levels.Get [CurrentScene] ?? PreviousScene;
			CurrentScene = next;
			StartCoroutine ("MoveNextLevelCoroutine");
			_isEndOfLevel = true;
		}
	}

	public void SetCurrentLevel (string levelName){
		CurrentScene = levelName;
	}

	public void PlayerDie(){
		if (!_isEndOfLevel) {
			StartCoroutine ("PlayerDieCoroutine");
			_isEndOfLevel = true;
		}
	}

	private IEnumerator PlayerDieCoroutine() {
		yield return new WaitForSeconds(2f);
		_isEndOfLevel = false;
		SceneManager.LoadScene (CurrentScene);
	}

	private IEnumerator MoveNextLevelCoroutine(){
		yield return new WaitForSeconds (2f);
		_isEndOfLevel = false;
		SceneManager.LoadScene (CurrentScene);
	}

	public void KillAllEnemies(){
		var enemies = GameObject.FindGameObjectsWithTag ("enemy");

		foreach (var enemy in enemies) {
			gameObject.GetComponent<Animator> ();
			var enemyController = enemy.GetComponent<EnemyMove> ();

			enemyController.Kill ();
		}
	}
}

