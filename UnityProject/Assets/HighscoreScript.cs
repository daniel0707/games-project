using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
