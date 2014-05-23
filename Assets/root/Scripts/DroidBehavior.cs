using UnityEngine;
using System.Collections;

public class DroidBehavior : MonoBehaviour {
	
	public float MOVEMENT_DELAY = 1f;
	public float FIRE_DELAY = 2f;
	public float MOVEMENT_SPEED = 2f;
	public float FIRE_SPEED = 60f;
	public GameObject LaserPrefab;

    public AudioClip LaserBlast;

	private float timeOfLastMovement;
	private float timeOfLastFire;
	private Vector3 droidPosOffset;
	
	// Use this for initialization
	void Start () {
		timeOfLastMovement = Time.time;
		Random.seed = 2;
		droidPosOffset = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		
		if(currentTime - timeOfLastMovement > MOVEMENT_DELAY) {
			timeOfLastMovement = currentTime;
			Move();
		}
		if(currentTime - timeOfLastFire > FIRE_DELAY) {
			timeOfLastFire = currentTime;
			Fire();
		}
		transform.position = Vector3.Lerp(transform.position, transform.position + droidPosOffset, 1f * Time.deltaTime);
		transform.Rotate(new Vector3(0, 1, 0), 30 * Time.deltaTime);
	}
	
	void Fire() {
		GameObject gamePlayer = GameObject.FindWithTag("Player");
		Vector3 playerPos = gamePlayer.transform.position + new Vector3(0f, 0.3f, 0f);
		Vector3 myPos = transform.position;
		Vector3 fireVec = playerPos - myPos;
        //Debug.Log(fireVec);
		
		GameObject laser = Instantiate(LaserPrefab, transform.position, transform.rotation) as GameObject;
		
		Ray ray = new Ray(myPos, fireVec);
		
		Rigidbody rb = laser.GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.AddForce(ray.direction * FIRE_SPEED);

        audio.PlayOneShot( LaserBlast );
	}
	
	void Move() {
		float x = (0.5f - Random.value) * MOVEMENT_SPEED * 2f;
		float y = (0.5f - Random.value) * MOVEMENT_SPEED;
		float z = (0.5f - Random.value) * MOVEMENT_SPEED;
		
		if(transform.position.x > 4) {
			x = -1f * Mathf.Abs(x);
		}
		if(transform.position.x < -4) {
			x = Mathf.Abs(x);
		}
		if(transform.position.y > 3) {
			y = -1f * Mathf.Abs(y);
		}
		if(transform.position.y < 0.75) {
			y = Mathf.Abs(y);
		}
		if(transform.position.z > 4) {
			z = -1f * Mathf.Abs(z);
		}
		if(transform.position.z < 2) {
			z = Mathf.Abs(z);
		}
		
		droidPosOffset = new Vector3(x, y, z);
	}
}
