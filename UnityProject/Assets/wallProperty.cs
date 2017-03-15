using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProperty : MonoBehaviour {
	
	private TheController tc;
	private float fallVel;

	// Used for initialization
	void Start () {
		tc = GameObject.Find ("GameController").GetComponent<TheController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		fallVel = tc.fallingVel;
		transform.Translate (0,fallVel, 0);
	}
}

