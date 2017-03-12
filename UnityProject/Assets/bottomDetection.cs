using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomDetection : MonoBehaviour {

	private theController tc;
	private GameObject penguin;

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Floor")) {
			if (other.transform.childCount > 0) {
				penguin.transform.parent = null;
			}
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
		penguin = GameObject.Find ("Penguin");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
