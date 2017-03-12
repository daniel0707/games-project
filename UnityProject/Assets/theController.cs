using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theController : MonoBehaviour {


	public int FloorID;
	public float fallingVel;
	private float[] floorZoneFallingSpeed = new float[] {-0.1f,-0.15f,-0.2f,-0.25f,-0.3f,-0.35f,-0.40f, -0.45f,-0.5f,-0.55f,-0.6f};
	private Vector2 spawnPoint;

	private int PlatformSize (int pID){
		int Stage;
		int SizeMIN;
		int SizeMAX;
		int Size;

		if (pID % 100 == 0) {
			Size = 15;
			return Size;
		}

		Stage = pID / 100;

		SizeMIN = 5 - Stage / 2;
		SizeMAX = 10 - Stage;
		Size = Random.Range (SizeMIN, SizeMAX + 1);

		return Size;
	}

	private float[] Position(){
		
		GameObject[] floorList;
		floorList = GameObject.FindGameObjectsWithTag("Floor");
		float highestFloorPosition = 0.0f;
		foreach (GameObject obj in floorList) {
			if (obj.transform.position.y > highestFloorPosition) {
				highestFloorPosition = obj.transform.position.y;
			}
		}
		highestFloorPosition += 42;
		int size = PlatformSize (FloorID);
		int xTemp = 42 - ((size * 6) +1);
		int xCoordinate = Random.Range (-48, xTemp);
		float[] myArray = new float[3]{xCoordinate,highestFloorPosition,size};

		return myArray;
	}

	public void spawnFloor(){
		float[] pos = Position ();

		spawnPoint = new Vector2(pos[0],pos[1]);

		float blockNR = pos[2] -1;

		int floorZone = FloorID / 100;

		fallingVel = floorZoneFallingSpeed[floorZone];

		string temp = "Prefabs/Floor" +floorZone +"A"+blockNR;

		GameObject platform = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
		blockProperty blockProp = platform.GetComponent<blockProperty> () as blockProperty;
		blockProp.level = FloorID;

		FloorID += 1;
	}

	public void spawnWallLeft(){
		float highestWallLeftPosition = 0.0f;
		GameObject[] wallLeftList;
		wallLeftList = GameObject.FindGameObjectsWithTag("WallLeft");
		foreach (GameObject obj in wallLeftList) {
			if (obj.transform.position.y > highestWallLeftPosition) {
				highestWallLeftPosition = obj.transform.position.y;
			}
		}
		highestWallLeftPosition += 42;
		Vector2 SPA = new Vector2 (-54, highestWallLeftPosition);
		int zoneA = (FloorID ) / 100;
		string tempA = "Prefabs/WallLeft" + zoneA + "A";
		GameObject wallLeft = Instantiate (Resources.Load (tempA, typeof(GameObject)), SPA, Quaternion.identity)as GameObject;
	}

	public void spawnWallRight(){
		float highestWallRightPosition = 0.0f;
		GameObject[] wallRightList;
		wallRightList = GameObject.FindGameObjectsWithTag("WallRight");
		foreach (GameObject obj in wallRightList) {
			if (obj.transform.position.y > highestWallRightPosition) {
				highestWallRightPosition = obj.transform.position.y;
			}
		}
		highestWallRightPosition += 42;
		Vector2 SPB = new Vector2 (48, highestWallRightPosition);
		int zoneB = (FloorID ) / 100;
		string tempB = "Prefabs/WallRight" + zoneB + "A";
		GameObject wallRight = Instantiate (Resources.Load (tempB, typeof(GameObject)), SPB, Quaternion.identity)as GameObject;
	}

	private void FirstSpawn(){
		int[] floorPositionY = new int[]{ -90, -48, -6, 36, 78, 120};
		int[] wallPositionY = new int[]{ -86, -44, -2, 40, 82, 124 };
		for (int i = 0; i < 6; i++) {
			//spawn floors
			float[] pos = Position ();
			spawnPoint = new Vector2 (pos[0], floorPositionY[i]);
			float blockNR = pos[2] - 1;
			int floorZone = FloorID / 100;
			string temp = "Prefabs/Floor" +floorZone +"A"+blockNR;
			GameObject platform = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
			blockProperty blockProp = platform.GetComponent<blockProperty> () as blockProperty;
			blockProp.level = FloorID;
			//spawn Left Walls
			spawnPoint = new Vector2 (-54, wallPositionY [i]);
			temp = "Prefabs/WallLeft" + floorZone + "A";
			GameObject wallLeft = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity)as GameObject;
			//spawn Right Walls
			spawnPoint = new Vector2 (48, wallPositionY [i]);
			temp = "Prefabs/WallRight" + floorZone + "A";
			GameObject wallRight = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity)as GameObject;
			//increment floorID
			FloorID += 1;
		}


	}

	// Use this for initialization
	void Start () {
		FloorID = 0;
		fallingVel = 0.0f;
		FirstSpawn();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}
}
