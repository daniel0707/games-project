using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockProperty : MonoBehaviour {
	
	PlatformEffector2D platEff;
	Renderer platRend;
	public int level;

	void turnOffEffector() {
		if (!platRend.isVisible && platEff.enabled) {
			platEff.enabled = false;
		}
	}

	void OnBecameVisible(){
		platEff.enabled = true;
	}
	// Use this for initialization
	void Start () {
		platEff = this.GetComponent <PlatformEffector2D> ();
		platRend = this.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame

	void FixedUpdate () {
		turnOffEffector ();
		transform.Translate (0, -0.1f, 0);
	}
}
