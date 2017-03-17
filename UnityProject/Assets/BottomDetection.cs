using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for destroying objects below camera view
public class BottomDetection : MonoBehaviour {

	private TheController tc;
	private GameObject penguin;

	//on collision we destroy object that collided and spawn a new one if required
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.CompareTag ("Floor")) {
			if (other.transform.childCount > 0) {
				penguin.transform.parent = null;
			}
			tc.SpawnFloor ();
			Destroy (other.gameObject);
		}
		if(other.gameObject.CompareTag("WallLeft")){
			tc.SpawnWallLeft();
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag ("WallRight")) {
			tc.SpawnWallRight ();
			Destroy (other.gameObject);
		}
		if(other.gameObject.CompareTag("Icicle")){
			Destroy (other.gameObject);
		}
	}

	// Used for initialization
	void Start () {
		tc = GameObject.Find ("GameController").GetComponent<TheController> ();
		penguin = GameObject.Find ("Penguin");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}
}
