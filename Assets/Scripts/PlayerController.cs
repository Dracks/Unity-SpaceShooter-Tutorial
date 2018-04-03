using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax;
	public float yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	public float speed=2;
	public float tilt;
	public Boundary bound;

	public GameObject shot;
	public Transform shotSpawn;

	private float nextFire;
	public float fireTimeElapsed=1;

	void Start(){

	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire=Time.time+fireTimeElapsed;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			AudioSource audio=GetComponent<AudioSource>();
			audio.enabled=true;
			audio.Play();
		}
	}

	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Rigidbody rigidBody = GetComponent<Rigidbody> ();
		rigidBody.velocity = new Vector3 (moveHorizontal, 0.0f, moveVertical)*speed;
		rigidBody.position = new Vector3 (
			Mathf.Clamp(rigidBody.position.x, bound.xMin, bound.xMax),
			0.0f,
			Mathf.Clamp(rigidBody.position.z, bound.yMin, bound.yMax)
		);

		rigidBody.rotation = Quaternion.Euler (0.0f, 0.0f, moveHorizontal * -tilt);
	}
}
