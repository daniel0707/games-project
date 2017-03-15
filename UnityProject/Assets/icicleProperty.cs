using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleProperty : MonoBehaviour {

	private TheController tc;
	private MovePenguin mp;
	private float fallVel;
	private GameObject icicle;
	private AudioSource gotHit;

	//on collision destroy icicle and decrease penguin health
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			mp.health--;
			gotHit.Play ();
			mp.gotHit = true;
			Destroy (icicle);
		}
	}

	void Start () {
		gotHit = GameObject.Find ("gotHit").GetComponent<AudioSource> ();
		tc = GameObject.Find ("GameController").GetComponent<TheController> ();
		mp = GameObject.Find ("Penguin").GetComponent<MovePenguin> ();	
		icicle = GameObject.FindGameObjectWithTag ("Icicle");
	}
		
	void FixedUpdate () {
		fallVel = tc.fallingVel * 3;
		transform.Translate (0,fallVel, 0);
	}
}
