using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour {
	
	private int floorID;
	private float fallSpeed;
	private int spawnPoint;
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
		int[] myArray = new myArray[2]{xCoordinate,168};

		return myArray;
	}

	public void spawnFloor(){
		
		Instantiate (platform);
	}

		// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
