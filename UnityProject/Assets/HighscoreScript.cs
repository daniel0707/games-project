using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script for attaching scores from database to the canvas
public class HighscoreScript : MonoBehaviour{

	public GameObject Score;
	public GameObject Name;
	public GameObject ID;


	public void SetScore(string name, string score, string ID){
		this.ID.GetComponent<Text> ().text = ID;
		this.Name.GetComponent<Text> ().text = name;
		this.Score.GetComponent<Text> ().text = score;
	}
}
