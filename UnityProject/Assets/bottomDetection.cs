using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomDetection : MonoBehaviour {

	private theController tc;


	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Floor")) {
			tc.spawnFloor ();
			Destroy (other.gameObject);
		}
		if(other.gameObject.CompareTag("WallLeft")){
			tc.spawnWallLeft();
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag ("WallRight")) {
			tc.spawnWallRight ();
			Destroy (other.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		tc = GameObject.Find ("GameController").GetComponent<theController> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
