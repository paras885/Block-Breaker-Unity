using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickCollider : MonoBehaviour {
	public int maxHits = 1;

	private BricksDAO bricksDAO = new BricksDAO ();
	private LevelManger levelManager;
	private int NUMBER_OF_BRICKS_ON_WHICH_GAME_END = 0;
	private int numberHits = 0;
	private GameObject smoke;

	public void setSmokeParticleSystem(GameObject ps) {
		this.smoke = ps;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		numberHits++;
		if (numberHits >= maxHits) {
			DestroyBrick ();
		}
			
		if (bricksDAO.getTotalNumberOfBricks () <= NUMBER_OF_BRICKS_ON_WHICH_GAME_END) {
			int indexLevel = SceneManager.GetActiveScene ().buildIndex;
			SceneManager.LoadScene (indexLevel);
		}
	}

	private void DestroyBrick () {
		GameObject smokePuff = (GameObject)Instantiate (smoke, this.gameObject.transform.position, Quaternion.identity);

		// To Change any module of ParticleSystem need to keep these modules
		// into different variable and then update
		var mainOfSmokePuff = smokePuff.GetComponent<ParticleSystem> ().main;
		mainOfSmokePuff.startColor = this.gameObject.GetComponent<SpriteRenderer>().color;

		Destroy (this.gameObject);
		bricksDAO.setTotalNumberOfBricks (bricksDAO.getTotalNumberOfBricks () - 1);
	}
}
