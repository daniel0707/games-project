using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenguin : MonoBehaviour {

	private float verticalSpeed = 0.0f;
	private float jumpSpeed = 50.0f;
	private float horizontalSpeed = 0.0f;

	private void movement(){
		if (Input.GetKey (KeyCode.UpArrow)) { //detect if the key down or up 
			verticalSpeed = jumpSpeed;
			//transform.Translate ( 0,jumpSpeed * Time.deltaTime,0);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			horizontalSpeed = -20;
			//transform.Translate ( horizontalSpeed * Time.deltaTime,0,0);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			horizontalSpeed = 20;
			//transform.Translate ( horizontalSpeed * Time.deltaTime,0,0);
		}


		verticalSpeed -= 1f;

		if (horizontalSpeed > 0) {
				horizontalSpeed -= 0.5f;
			}
		if (horizontalSpeed < 0) {
			horizontalSpeed += 0.5f;
		}
			transform.Translate(horizontalSpeed* Time.deltaTime,verticalSpeed* Time.deltaTime,0);
		}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		movement ();
		
	}
}
