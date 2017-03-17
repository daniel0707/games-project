using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//property of each platform
public class BlockProperty : MonoBehaviour {
	
	//references
	private TheController cont;
	private PlatformEffector2D platEff;
	private Renderer platRend;

	private float fallVel;
	public int level;

	private void turnOffEffector() {
		if (!platRend.isVisible && platEff.enabled) {
			platEff.enabled = false;
		}
	}

	private void OnBecameVisible(){
		platEff.enabled = true;
	}
		
	// Used for initialization
	void Start () {
		cont = GameObject.Find ("GameController").GetComponent<TheController> ();
		platEff = this.GetComponent <PlatformEffector2D> ();
		platRend = this.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		turnOffEffector ();
		fallVel = cont.fallingVel;
		transform.Translate (0,fallVel, 0);
	}
}

