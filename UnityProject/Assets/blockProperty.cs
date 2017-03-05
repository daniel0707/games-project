using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockProperty : MonoBehaviour {
	
	PlatformEffector2D platEff;

	void OnBecameInvisible() {
		platEff.enabled = false;
	}

	void OnBecameVisible(){
		platEff.enabled = true;
	}
	// Use this for initialization
	void Start () {
		platEff = this.GetComponent <PlatformEffector2D> ();

	}
	
	// Update is called once per frame

	void FixedUpdate () {
		transform.Translate (0, -0.1f, 0);
	}
}
