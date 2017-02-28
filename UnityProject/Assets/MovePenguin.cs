using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenguin : MonoBehaviour {

	private float jumpSpeed = 2160000f;
	private float comboSpeed = 0.3f;
	private float maxSpeed = 100f;
	private float minSpeed = -100f;
	private float sideSpeed = 10f;
	private bool isOnFloor = false;
	private Rigidbody2D penguinBody;


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor") {
			isOnFloor = true;
		}
	}
	private void movement(){
		if (Input.GetKey (KeyCode.UpArrow) && isOnFloor) { //detect if the key down or up and is on floor
			penguinBody.AddForce(Vector2.up * jumpSpeed * comboSpeed);
			//transform.Translate(0,jumpSpeed * Time.deltaTime,0);
			isOnFloor = false;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (penguinBody.velocity.x > minSpeed) {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x - sideSpeed, penguinBody.velocity.y);
			} else {
				penguinBody.velocity = new Vector2 (minSpeed, penguinBody.velocity.y);
			}
		}
				
		if (Input.GetKey (KeyCode.RightArrow)) {
			if (penguinBody.velocity.x < maxSpeed) {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x + sideSpeed, penguinBody.velocity.y);
			} else {
				penguinBody.velocity = new Vector2 (maxSpeed, penguinBody.velocity.y);
			}
		}
					
	}

	// Use this for initialization
	void Start () {
		penguinBody = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		movement ();
		
	}
}
