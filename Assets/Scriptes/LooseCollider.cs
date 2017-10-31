using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LooseCollider : MonoBehaviour {

	public LevelManger levelManger;

	void OnTriggerEnter2D (Collider2D collider) {
		levelManger.loadLevel ("Lose_Screen");
	}

	void OnCollisionEnter2D (Collision2D collision) {
		print ("Collision");
	}
}
