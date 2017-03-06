using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenguin : MonoBehaviour {
	
	public GUIText scoreText;

	private float currentLevel = 0;
	private float highestLevel = 0;
	private float previousLevel = 0;
	public float score = 0;

	private float jumpSpeed = 150f;
	private float comboSpeed = 1.0f;
	private int comboScore = 0;
	private float maxSpeed = 100f;
	private float minSpeed = -100f;
	private float sideSpeed = 10f;

	private Rigidbody2D penguinBody;

	public GameObject[] objectlist;

	bool grounded = false;
	public LayerMask whatIsGround;
	public Transform pointA;
	public Transform pointB;


	public bool isGrounded(){
		grounded = Physics2D.OverlapArea (pointA.position,pointB.position, whatIsGround);
		return grounded;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Floor") {
			transform.parent = other.transform;

			//Add to score if player reached a higher level
			if (isGrounded ()) {
				blockProperty bP = other.gameObject.GetComponent<blockProperty> () as blockProperty;
				currentLevel = bP.level;
				if (currentLevel > highestLevel + 1) {
					comboScore += 1;
				} else {
					comboScore = 0;
				}

				if (currentLevel > highestLevel) {
					highestLevel = currentLevel;
				}
				if (currentLevel == highestLevel) {
					score += (10 + comboScore) * (currentLevel - previousLevel);
				}
				if (currentLevel > previousLevel) {
					previousLevel = currentLevel;
				}
				scoreText.text = "Score: " + score;
			}
		}
	}

	void OncollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Floor") {
			transform.parent = null;
		}
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
		if (penguinBody.transform.position.y + penguinBody.velocity.y * Time.deltaTime >= 78) {
			
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
		scoreText = GameObject.Find ("ScoreView").GetComponent<GUIText> ();
		scoreText.text = "Score: " + score;
	}
	
	// Update is called once per frame, fixed framerate
	void FixedUpdate () {
		movement ();
	}
	// Update is called once per frame, frame pushed when ready
	void Update(){
		fixPos ();
	}
}
