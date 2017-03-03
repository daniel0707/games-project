﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theController : MonoBehaviour {


	//Vector2 startPosition = new Vector2 (0,-10);

	private int floorID = 0;
	public int FallSpeed = -1;
	private Vector2 spawnPoint;
	GameObject platform;

	private int PlatformSize (int pID){
		int Stage;
		int SizeMIN;
		int SizeMAX;
		int Size;

		if (pID % 100 == 0) {
			Size = 17;
			return Size;
		}

		Stage = pID / 100;

		SizeMIN = 5 - Stage / 2;
		SizeMAX = 10 - Stage;
		Size = Random.Range (SizeMIN, SizeMAX + 1);

		return Size;
	}

	private int[] Position(){
		int size = PlatformSize (floorID);
		int xTemp = 42 - (size * 6) +1;
		int xCoordinate = Random.Range (-48, xTemp);
		int[] myArray = new int[2]{xCoordinate,112};

		return myArray;
	}

	public void spawnFloor(){
		int platSize = PlatformSize (floorID);

		int[] pos = Position ();

		spawnPoint = new Vector2(pos[0],pos[1]);

		int blockNR = platSize - 1;

		string temp = "Prefabs/Block" + blockNR;

		platform = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;

		floorID += 1;
	}


	private void FirstSpawn(){
		int[] positionY = new int[]{ -90, -66, -42, -18, 6, 30, 54, 78, 102 };

		for (int i = 0; i < 9; i++) {

			int platSize = PlatformSize (floorID);

			int[] pos = Position ();

			spawnPoint = new Vector2 (pos[0], positionY[i]);

			int blockNR = platSize - 1;

			string temp = "Prefabs/Block" + blockNR;

			//platform = GameObject.Find (temp);

			platform = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;

			floorID += 1;
		}
	}

	// Use this for initialization
	void Start () {
		//GameObject platform1 = Instantiate (Resources.Load ("Prefabs/Block0", typeof(GameObject)),startPosition,Quaternion.identity) as GameObject;
		FirstSpawn();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}
}
