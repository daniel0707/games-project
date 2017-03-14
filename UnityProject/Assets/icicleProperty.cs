using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icicleProperty : MonoBehaviour {

	private theController tc;
	private MovePenguin mp;
	private float fallVel;
	private GameObject icicle;
	private AudioSource gotHit;


	// Use this for initialization
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			mp.Health--;

			gotHit.Play ();
			mp.gotHit = true;
			Destroy (icicle);
		}
	}

	void Start () {
		gotHit = GameObject.Find ("gotHit").GetComponent<AudioSource> ();
		tc = GameObject.Find ("GameController").GetComponent<theController> ();
		mp = GameObject.Find ("Penguin").GetComponent<MovePenguin> ();	
		icicle = GameObject.FindGameObjectWithTag ("Icicle");
	}


	// Update is called once per frame

	void FixedUpdate () {
		fallVel = tc.fallingVel * 3;
		transform.Translate (0,fallVel, 0);
	}
}
