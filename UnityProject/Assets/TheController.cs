using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//gameworld controller
public class TheController : MonoBehaviour {

	public int floorID;
	public float fallingVel;
	private float[] floorZoneFallingSpeed = new float[] {-0.2f,-0.3f,-0.4f,-0.5f,-0.6f,-0.7f,-0.8f, -0.9f,-1.0f,-1.1f,-1.2f};
	private Vector2 spawnPoint;

	private int platformSize (int pID){
		int stage;
		int sizeMIN;
		int sizeMAX;
		int size;

		if (pID % 50 == 0) {
			size = 15;
			return size;
		}

		stage = pID / 50;
		sizeMIN = 5 - stage / 2;
		sizeMAX = 10 - stage;
		size = Random.Range (sizeMIN, sizeMAX + 1);

		return size;
	}

	private float[] position(){
		GameObject[] floorList;
		floorList = GameObject.FindGameObjectsWithTag("Floor");
		float highestFloorPosition = 0.0f;
		foreach (GameObject obj in floorList) {
			if (obj.transform.position.y > highestFloorPosition) {
				highestFloorPosition = obj.transform.position.y;
			}
		}
		highestFloorPosition += 42;
		int size = platformSize (floorID);
		int xTemp = 42 - ((size * 6) +1);
		int xCoordinate = Random.Range (-48, xTemp);
		float[] myArray = new float[3]{xCoordinate,highestFloorPosition,size};

		return myArray;
	}

	public void SpawnFloor(){
		float[] pos = position ();
		spawnPoint = new Vector2(pos[0],pos[1]);
		float blockNR = pos[2] -1;

		int floorZone = floorID / 50;
		fallingVel = floorZoneFallingSpeed[floorZone];
		string temp = "Prefabs/Floor" +floorZone +"A"+blockNR;

		GameObject platform = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
		BlockProperty blockProp = platform.GetComponent<BlockProperty> () as BlockProperty;
		blockProp.level = floorID;

		floorID += 1;

		//in case floor got destroyed but didn't instantiate a new one
		if (pos [1] < 96) {
			SpawnFloor ();
		}
	}

	public void SpawnWallLeft(){
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
		int zoneA = (floorID ) / 50;
		string tempA = "Prefabs/WallLeft" + zoneA + "A";
		GameObject wallLeft = Instantiate (Resources.Load (tempA, typeof(GameObject)), SPA, Quaternion.identity)as GameObject;
	}

	public void SpawnWallRight(){
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
		int zoneB = (floorID ) / 50;
		string tempB = "Prefabs/WallRight" + zoneB + "A";
		GameObject wallRight = Instantiate (Resources.Load (tempB, typeof(GameObject)), SPB, Quaternion.identity)as GameObject;
	}

	private void firstSpawn(){
		int[] floorPositionY = new int[]{ -90, -48, -6, 36, 78, 120};
		int[] wallPositionY = new int[]{ -86, -44, -2, 40, 82, 124 };
		for (int i = 0; i < 6; i++) {
			
			//spawn floors
			float[] pos = position ();
			spawnPoint = new Vector2 (pos[0], floorPositionY[i]);
			float blockNR = pos[2] - 1;
			int floorZone = floorID / 50;
			string temp = "Prefabs/Floor" +floorZone +"A"+blockNR;
			GameObject platform = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity) as GameObject;
			BlockProperty blockProp = platform.GetComponent<BlockProperty> () as BlockProperty;
			blockProp.level = floorID;

			//spawn Left Walls
			spawnPoint = new Vector2 (-54, wallPositionY [i]);
			temp = "Prefabs/WallLeft" + floorZone + "A";
			GameObject wallLeft = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity)as GameObject;

			//spawn Right Walls
			spawnPoint = new Vector2 (48, wallPositionY [i]);
			temp = "Prefabs/WallRight" + floorZone + "A";
			GameObject wallRight = Instantiate (Resources.Load (temp, typeof(GameObject)), spawnPoint, Quaternion.identity)as GameObject;

			//increment floorID
			floorID += 1;
		}
	}


	// Used for initialization
	void Start () {
		floorID = 0;
		fallingVel = 0.0f;
		firstSpawn();
	}

	// Update is called once per frame
	void FixedUpdate () {
	}
}
