using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Paddle paddle;
	private Vector3 paddleToBall;
	private bool hasStarted = false;
	private Rigidbody2D ballRigidbody2D;


	// Use this for initialization
	void Start () {
		paddleToBall = this.transform.position - paddle.transform.position;
		ballRigidbody2D = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!hasStarted) {
			this.transform.position = paddleToBall + paddle.transform.position;
			if (Input.GetMouseButtonDown (0)) {
				hasStarted = true;
				ballRigidbody2D.velocity = new Vector2 (2f, 10f);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (hasStarted) {
			Vector2 tweak = new Vector2 (Random.Range (0f, 0.3f), Random.Range (0f, 0.3f));
			ballRigidbody2D.velocity += tweak;
		}
	}
}
