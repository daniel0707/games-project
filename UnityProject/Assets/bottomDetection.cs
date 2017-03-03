using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomDetection : MonoBehaviour {

	private theController tc;
	void OnCollisionEnter2D (Collision2D other) {
		
		tc.spawnFloor ();
		Destroy(other.gameObject);
	}
	// Use this for initialization
	void Start () {
		tc = GameObject.Find ("GameController").GetComponent<theController> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
