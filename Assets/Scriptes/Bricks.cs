using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bricks : MonoBehaviour {

	public Sprite fullBrick;
	public GameObject smoke;

	private float startingPositionX = 1.5f;
	private float startingPositionY = 7.0f;
	private float minimumXGap = 1.0f;
	private float minimumYGap = 0.5f;
	private float maxX = 15.0f;
	private float maxY = 10.0f;

	private float[] minimumXVarianceValues = { 0.0f, 0.5f };
	private float[] maximumXVarianceValues = { 0.0f, 0.5f };
	private int[] maxHits = { 1, 2, 3 };

	private Color[] colors = { Color.blue, Color.green, Color.red };
	private ParticleSystemShapeType boxShape = ParticleSystemShapeType.Box;

	private string BRICK_PREFIX = "Brick_";

	// Because it is 2D Game :)
	private float fixedPositionZ = 0.0f;

	private string getGameObjectName (int brickNumber) {
		return BRICK_PREFIX + brickNumber.ToString ();
	}

	private void createEmissionForSmoke (ParticleSystem particleSystem) {
		var em = particleSystem.emission;
		em.rateOverDistance = 10;
		em.rateOverDistance = 0;
	}

	private void createShapeForSmoke (ParticleSystem particleSystem) {
		var shape = particleSystem.shape;
		shape.shapeType = boxShape;
		shape.position = new Vector3 (0f, 0f, 0f);
		shape.rotation = new Vector3 (0f, 0f, 0f);
		shape.scale = new Vector3 (1f, 1f, 1f);
		shape.randomDirectionAmount = 0;
		shape.randomPositionAmount = 0;
		shape.sphericalDirectionAmount = 0;
	}

	private void createMainForSmoke (ParticleSystem particleSystem) {
		var man = particleSystem.main;
		man.duration = 1.0f;
		man.loop = false;
		man.startLifetime = 1.0f;
		man.startSpeed = 5.0f;
		man.startSize = 1.0f;
		man.simulationSpeed = 1.0f;
		man.playOnAwake = false;
		man.emitterVelocityMode = ParticleSystemEmitterVelocityMode.Rigidbody;
		man.maxParticles = 1000;
	}

	private void createBrick(float xPoint, float yPoint, int brickNumber) {
		// Set Game Object Position 
		GameObject go = new GameObject (getGameObjectName (brickNumber));
		go.transform.position = new Vector3 (xPoint, yPoint, fixedPositionZ);

		int randomPositionForColor = (int)Random.Range (0, 3);

		// Set brick sprite
		SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer> ();
		spriteRenderer.sprite = fullBrick;
		spriteRenderer.color = colors[randomPositionForColor];

		// Add Box Collider
		go.AddComponent<BoxCollider2D> ();

		// Add script and required variables.
		BrickCollider brickCollider = go.AddComponent<BrickCollider> ();
		brickCollider.maxHits = maxHits[randomPositionForColor];
		brickCollider.setSmokeParticleSystem (smoke);

//		// Add a ParticleSystem 
//		ParticleSystem particleSystem = go.AddComponent<ParticleSystem> ();
//		particleSystem.Stop ();
//		particleSystem = smoke;

//		createEmissionForSmoke (particleSystem);
//		createShapeForSmoke (particleSystem);
//		createMainForSmoke (particleSystem);

		// Add a ParticleSystemRenderer
//		ParticleSystemRenderer particleSystemRenderer = go.AddComponent<ParticleSystemRenderer> ();
//		createRendererForSmoke (particleSystemRenderer);
	}

	// Use this for initialization
	void Start () {
		int brickNumber = 0;
		float minimumXVarianceValue = 0.00f;
		float maximumXVarianceValue = 0.00f;
		for (float yPoint = startingPositionY; yPoint < maxY; yPoint += minimumYGap) {
			for (float xPoint = startingPositionX + minimumXVarianceValue; xPoint < maxX - maximumXVarianceValue; xPoint += minimumXGap) {
				createBrick (xPoint, yPoint, brickNumber);
				brickNumber++;
			}
			int indexOfCurrentScene = SceneManager.GetActiveScene ().buildIndex;
			minimumXVarianceValue += minimumXVarianceValues [indexOfCurrentScene - 1];
			maximumXVarianceValue += maximumXVarianceValues [indexOfCurrentScene - 1];
		}
		BricksDAO bricksDAO = new BricksDAO ();
		bricksDAO.setTotalNumberOfBricks (brickNumber);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
