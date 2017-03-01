using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockProperty : MonoBehaviour {

	private int floorID;
	private float fallSpeed;
	GameObject platform;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, -1, 0);
	}
}
