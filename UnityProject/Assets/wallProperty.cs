using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallProperty : MonoBehaviour {
	
	private theController tc;

	private float fallVel;



	// Use this for initialization
	void Start () {
		tc = GameObject.Find ("GameController").GetComponent<theController> ();
	}
	
	// Update is called once per frame

	void FixedUpdate () {
		fallVel = tc.fallingVel;
		transform.Translate (0,fallVel, 0);
	}
}
