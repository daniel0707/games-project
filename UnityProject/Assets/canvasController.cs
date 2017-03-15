using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//control canvas state, set visible to show scores
public class CanvasController : MonoBehaviour {
	public GameObject canvasObject;

	public void MakeActive(){
		canvasObject.SetActive (true);
	}

	void Start () {
	}
	void Update () {
	}
}
