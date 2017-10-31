using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	public bool isAutoPlay = false;

	private static float minXPosition = 0.5f;
	private static float maxXPosition = 15.5f;

	private Ball ball;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAutoPlay) {
			moveWithMouse ();
		} else {
			AutoPlay ();
		}
	}

	private void AutoPlay () {
		float xPosition = ball.transform.position.x;
		xPosition = clamp (xPosition);
		this.transform.position = new Vector3 (xPosition, this.transform.position.y, this.transform.position.z);
	}

	private void moveWithMouse () {
		float xPosition = (Input.mousePosition.x / Screen.width) * 16f;
		xPosition = clamp (xPosition);
		this.transform.position = new Vector3 (xPosition, this.transform.position.y, this.transform.position.z);
		print ("xPosition : " + xPosition);
	}

	private float clamp(float xPosition) {
		return (xPosition < minXPosition ? minXPosition : (xPosition > maxXPosition ? maxXPosition : xPosition));
	}
}
