using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reset : MonoBehaviour {

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
