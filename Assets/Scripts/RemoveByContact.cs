﻿using UnityEngine;
using System.Collections;

public class RemoveByContact : MonoBehaviour {
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController=gameControllerObject.GetComponent<GameController>();
			if (gameController==null){
				Debug.Log("Cannot find 'GameController' script");
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary") {
			return;
		}
		Destroy (other.gameObject);
		Destroy (gameObject);
		Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
			Debug.Log (this);
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.LooseLive();
		} else {
			gameController.AddScore (scoreValue);
		}
	}
}
