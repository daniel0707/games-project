using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomDetection : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		Debug.Log (other.gameObject.name);
		Destroy(other.gameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
