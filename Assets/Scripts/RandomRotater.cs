using UnityEngine;
using System.Collections;

public class RandomRotater : MonoBehaviour {

	public float tumble;
	void Start(){
		Rigidbody rigidbody=GetComponent<Rigidbody> ();
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;

	}
}
