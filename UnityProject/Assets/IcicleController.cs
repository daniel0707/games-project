using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//control of Icicle fall events
public class IcicleController : MonoBehaviour {
	
	private GameObject IW;
	private Vector2 position;
	private MovePenguin mp;

	//spawns icicles in random amount of time and in random X position
	//icicle warning preceeds the icicle
	private void icicleFall(){
		if (mp.isAlive && mp.firstJump) {
			float spawn = Random.Range (-46f, 46f);
			position = new Vector2 (spawn, 96);
			icicleWarningON (position);
			Invoke ("icicleWarningOFF", 3f);
			Invoke ("icicleSpawn", 4f);
			Invoke ("icicleFall", icicleTimer ());
		}
	}

	private void icicleSpawn(){
		GameObject icicle = Instantiate (Resources.Load ("Prefabs/Icicle", typeof(GameObject)), position, Quaternion.identity) as GameObject;
	}

	private float icicleTimer(){
		return Random.Range (8f, 18f);
	}

	private void icicleWarningON(Vector2 pos){
		IW.SetActive (true);
		IW.transform.position = pos;
	}

	private void icicleWarningOFF(){
		IW.SetActive (false);
	}


	void Start () {
		mp = GameObject.Find ("Penguin").GetComponent<MovePenguin> ();
		IW = GameObject.Find ("IcicleWarning");
		Invoke ("icicleFall", 5);
	}
	void Update () {
	}
}
