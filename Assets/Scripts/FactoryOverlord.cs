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

	public void FinishLevel(){
		var enemies = GameObject.FindGameObjectsWithTag ("enemy");
		var bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		var shooters = GameObject.FindGameObjectsWithTag ("Shooter");

		foreach (var enemy in enemies) {
			var enemyController = enemy.GetComponent<EnemyMove> ();

			enemyController.Kill ();
		}

		foreach (var bullet in bullets) {
			var bulletController = bullet.GetComponent<BulletController> ();

			bulletController.Kill ();
		}

		foreach (var shooter in shooters) {
			var shooterController = shooter.GetComponent<ShooterController> ();

			shooterController.Disable ();
		}
	}
}

