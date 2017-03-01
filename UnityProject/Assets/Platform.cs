using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour {
	
	private int floorID = 1;
	public float fallSpeed = 20f;
	private Vector2 spawnPoint;
	GameObject platform;

	private int PlatformSize (int pID){
		int Stage;
		int SizeMIN;
		int SizeMAX;
		int Size;

		Stage = pID / 100;

		SizeMIN = 5 - Stage / 2;
		SizeMAX = 10 - Stage;
		Size = Random.Range (SizeMIN, SizeMAX + 1);

		return Size;
	}

	private int[] Position(){
		int size = PlatformSize (floorID);
		int xTemp = 48 - size * 6;
		int xCoordinate = Random.Range (-48, xTemp);
		int[] myArray = new int[2]{xCoordinate,168};

		return myArray;
	}

	public void spawnFloor(){
		int platSize = PlatformSize (floorID);

		int[] pos = Position ();

		spawnPoint = new Vector2(pos[0],pos[1]);

		string temp = "Block" + platSize;

		platform = GameObject.Find (temp);
			
		Instantiate (platform,spawnPoint,Quaternion.identity);

		floorID += 1;
	}

	public void firstSpawn (){


	}

		// Use this for initialization
	void Start () {
		firstSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
