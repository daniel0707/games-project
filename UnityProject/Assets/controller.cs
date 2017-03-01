using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

	Vector2 startPosition = new Vector2 (0,-10);

	// Use this for initialization
	void Start () {
		GameObject platform1 = Instantiate (Resources.Load ("Prefabs/Block0", typeof(GameObject)),startPosition,Quaternion.identity) as GameObject;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
