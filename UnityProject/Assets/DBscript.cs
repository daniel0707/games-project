using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public class DBscript : MonoBehaviour {

	public bool scoreEntered = false;
	
	private string conn;

	public List<Highscore>highScores = new List<Highscore>();

	public GameObject scorePrefab;

	public Transform scoreParent;

	public int topRanks;

	public int saveScores;

	public InputField playerName;

	private MovePenguin mp;

	public GameObject nameDialog;


	private void GetScores(){
		highScores.Clear ();

		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "SELECT ID,Name,Score FROM HighScore";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			highScores.Add(new Highscore(reader.GetInt32(0),reader.GetString(1),reader.GetInt32(2)));
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		highScores.Sort ();
	}

	public void ShowScores(){
		
		GetScores ();

		foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score")) {
			Destroy (score);
		}

		for (int i = 0; i < topRanks; i++) {
			if (i < highScores.Count) {
				GameObject tmpObj = Instantiate (scorePrefab);
				Highscore tmpScore = highScores [i];
				tmpObj.GetComponent<HighscoreScript> ().SetScore (tmpScore.Name, tmpScore.EndScore.ToString (), "#" + (i + 1) );
				tmpObj.transform.SetParent (scoreParent);
				tmpObj.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);


			}
		}
	}

	private void InsertScore(string name, int newScore){
		int hsCount = highScores.Count;
		GetScores ();
		if (highScores.Count > 0) {
			Highscore lowestScore = highScores [highScores.Count - 1];
			if (lowestScore != null && saveScores > 0 && highScores.Count >= saveScores && newScore > lowestScore.EndScore) {
				DeleteScore (lowestScore.ID);
				hsCount--;
			}
		}
		if (hsCount < saveScores) {
			IDbConnection dbconn;
			dbconn = (IDbConnection) new SqliteConnection(conn);
			dbconn.Open(); //Open connection to the database.
			IDbCommand dbcmd = dbconn.CreateCommand();
			string sqlQuery = string.Format("INSERT INTO HighScore(Name,Score)VALUES(\'{0}\',\'{1}\')",name,newScore);
			dbcmd.CommandText = sqlQuery;
			dbcmd.ExecuteScalar ();
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;
		}
	}

	private void DeleteScore(int rank){
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = string.Format("DELETE FROM HighScore WHERE ID = \'{0}\'",rank);
		dbcmd.CommandText = sqlQuery;
		dbcmd.ExecuteScalar ();
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
	}

	private void DeleteExtraScore(){

		GetScores ();

		if (saveScores < highScores.Count) {
			int deleteCount = highScores.Count - saveScores;
			highScores.Reverse ();

			IDbConnection dbconn;
			dbconn = (IDbConnection) new SqliteConnection(conn);
			dbconn.Open(); //Open connection to the database.
			IDbCommand dbcmd = dbconn.CreateCommand();

			for (int i = 0; i < deleteCount; i++) {
				string sqlQuery = string.Format("DELETE FROM HighScore WHERE ID = \'{0}\'",highScores[i].ID);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
			}
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;
		}
	}

	public void EnterName(){
		if (playerName.text != string.Empty) {
			
			int score = (int)mp.Score;
			InsertScore (playerName.text, score);
			playerName.text = string.Empty;

			ShowScores ();
			scoreEntered = true;
			nameDialog.SetActive (false);

		}
	}

	public bool newHighScore(){
		if (highScores.Count < saveScores) {
			return true;
		} else {
			for (int i = 0; i < highScores.Count; i++) {
				if (mp.Score > highScores [i].EndScore) {
					return true;
				}
			}
		}
		return false;
	}
		
	private void createDefaultDB(){
	}
	// Use this for initialization
	void Start () {
		
		conn = "URI=file:" + System.IO.Path.Combine(Application.streamingAssetsPath, "Database.db"); //Path to database.
		mp = GameObject.Find("Penguin").GetComponent<MovePenguin>();
		//DeleteExtraScore ();

		ShowScores ();
		//sql stuff
		/*
		string conn = "URI=file:" + Application.dataPath + "/DB/Database.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "SELECT ID,Name,Score FROM HighScore";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			int ID = reader.GetInt32(0);
			string Name = reader.GetString(1);
			int Score = reader.GetInt32(2);

		}
		reader.Close();
		reader = null;
		*/


		//closing stuff
		/*
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
		*/
	}
	
	// Update is called once per frame
	void Update () {
		//something for showing the end game screen
		//on new score do: nameDialog.SetActive(true);
	}
}
