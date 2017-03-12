using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleController : MonoBehaviour {
	
	private GameObject IW;
	private Vector2 position;

	private void IcicleFall(){
		float spawn = Random.Range (-46f, 46f);
		position = new Vector2 (spawn, 96);
		IcicleWarningON (position);
		Invoke ("IcicleWarningOFF", 3f);
		Invoke ("IcicleSpawn", 4f);
		Invoke ("IcicleFall", IcicleTimer ());
	}

	private void IcicleSpawn(){
		GameObject icicle = Instantiate (Resources.Load ("Prefabs/Icicle", typeof(GameObject)), position, Quaternion.identity) as GameObject;
	}

	private float IcicleTimer(){
		return Random.Range (8f, 18f);
	}

	private void IcicleWarningON(Vector2 pos){
		IW.SetActive (true);
		IW.transform.position = pos;
	}
	private void IcicleWarningOFF(){
		IW.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		IW = GameObject.Find ("IcicleWarning");
		Invoke ("IcicleFall", 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
