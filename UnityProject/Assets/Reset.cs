using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//reloads game scene, should be attached to a button
public class Reset : MonoBehaviour {

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	void Start () {
	}
	void Update () {
	}
}
