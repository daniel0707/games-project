using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenguin : MonoBehaviour {

	private float jumpSpeed = 400f;
	private float comboSpeed = 0.3f;
	private float maxSpeed = 100f;
	private float minSpeed = -100f;
	private float sideSpeed = 10f;
	private Rigidbody2D penguinBody;

	public GameObject[] objectlist;

	bool grounded = false;
	public LayerMask whatIsGround;
	public Transform pointA;
	public Transform pointB;



	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Floor") {
			transform.parent = other.transform;
		}
	}

	void OncollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Floor") {
			transform.parent = null;
		}
	}

	public bool isGrounded(){
		grounded = Physics2D.OverlapArea (pointA.position,pointB.position, whatIsGround);
		return grounded;
	}


	private bool isFalling(){
		if (penguinBody.velocity.y <= 0 ) {
			return true;
		} else {
			return false;
		}
	}

	private void movement(){

		if ((Input.GetKey (KeyCode.UpArrow)) && (isGrounded()) && (isFalling())) { 
			penguinBody.velocity = new Vector2 (penguinBody.velocity.x, jumpSpeed * comboSpeed);
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

	private void fixPos (){
		if (penguinBody.transform.position.y > 78) {
			
			penguinBody.transform.position = new Vector2 (penguinBody.transform.position.x, 78);

			objectlist = GameObject.FindGameObjectsWithTag("Floor");

			foreach (GameObject obj in objectlist) {
				obj.transform.Translate (0,- penguinBody.velocity.y * Time.deltaTime, 0);
			}
		}
	}




	// Use this for initialization
	void Start () {
		penguinBody = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		movement ();
	}
	void Update(){
		fixPos ();
	}
}
