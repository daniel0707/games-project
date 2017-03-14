using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenguin : MonoBehaviour {

	private SpriteRenderer penguinRenderer;
	private canvasController CC;
	private DBscript DBref;
	private bool bouncedWall;
	public bool isAlive = true;
	private Animator anim;
	private theController controlRef;
	public bool firstJump = false;

	public bool gotHit;
	public int fadeCount = 0;
	public float alphaState = 1f;
	public float order = 0;
	public int Health = 3;
	public int Cooldown = 0;
	public float LastSpeed;
	public float CurrentSpeed;
	public bool FacingRight = true;
	public GUIText ScoreText;

	private float currentLevel = 0f;
	private float highestLevel = 0f;
	private float previousLevel = 0f;
	public float Score = 0f;

	private float jumpSpeed = 200f;
	private float comboSpeed = 1.0f;
	private int comboScore = 0;
	private float maxComboSpeed = 2.0f;
	private float maxSpeed = 100f;
	private float minSpeed = -100f;
	private float sideSpeed = 10f;

	private Rigidbody2D penguinBody;

	public List<GameObject> Objectlist= new List<GameObject>();

	private bool grounded = false;
	public LayerMask WhatIsGround;
	public Transform PointA;
	public Transform PointB;

	public void getLastVelocity(){
		LastSpeed = CurrentSpeed;
		CurrentSpeed = penguinBody.velocity.x;
	}
	public bool isGrounded(){
		grounded = Physics2D.OverlapArea (PointA.position,PointB.position, WhatIsGround);
		return grounded;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Floor") {
			transform.parent = other.transform;

			//Add to score if player reached a higher level
			if (isGrounded ()) {
				blockProperty bP = other.gameObject.GetComponent<blockProperty> () as blockProperty;
				currentLevel = bP.level;
				if (currentLevel == 500) {
					isAlive = false;
				}
				if (currentLevel > highestLevel + 1) {
					comboScore += 1;
				} else {
					comboScore = 0;
				}
				if (currentLevel > highestLevel) {
					if (!bouncedWall && comboSpeed>1.0f) {
						comboSpeed -= 0.1f;
					}
					highestLevel = currentLevel;
				} else {
					comboSpeed = 1.0f;
				}
				if (currentLevel == highestLevel) {
					Score += (10 + comboScore) * (currentLevel - previousLevel);
				}
				if (currentLevel > previousLevel) {
					previousLevel = currentLevel;
				}
				ScoreText.text = "Score: " + Score;
			}
		}
		if (other.gameObject.tag == "WallLeft" || other.gameObject.tag == "WallRight") {
			bouncedWall = true;
			other.gameObject.GetComponent<AudioSource>().Play();
			Flip ();
		}
	}

	void OncollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Floor") {
			transform.parent = null;
		}
	}
		
	private bool isFalling(){
		if (penguinBody.velocity.y <= 0.1f) {
			return true;
		} else {
			return false;
		}
	}

	private void movement(){
		maxSpeed = 100f * comboSpeed;
		minSpeed = -100f * comboSpeed;
		sideSpeed = 10f * comboSpeed;

		if ((Input.GetKey (KeyCode.UpArrow)) && (isGrounded()) && (isFalling())) {
			GetComponent<AudioSource>().Play();
			if (penguinBody.velocity.x <= 100f) {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x, jumpSpeed);
			} else {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x, jumpSpeed * comboSpeed);
			}
			if (firstJump == false) {
				controlRef.fallingVel = -0.1f;
				firstJump = true;
			}
		}
			
		if (Input.GetKey (KeyCode.LeftArrow)&&Cooldown == 0) {
			if (penguinBody.velocity.x > minSpeed) {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x - sideSpeed, penguinBody.velocity.y);
			} else {
				penguinBody.velocity = new Vector2 (minSpeed, penguinBody.velocity.y);
			}
		}
				

		if (Input.GetKey (KeyCode.RightArrow) && Cooldown == 0) {

			if (penguinBody.velocity.x < maxSpeed) {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x + sideSpeed, penguinBody.velocity.y);
			} else {
				penguinBody.velocity = new Vector2 (maxSpeed, penguinBody.velocity.y);
			}
		}
					
	}
	public void turnCorrect (){
		if (penguinBody.velocity.x > 0) {
			FacingRight = true;
			penguinBody.transform.localScale = new Vector3 (1, 1, 1);
		}
		if (penguinBody.velocity.x < 0) {
			penguinBody.transform.localScale = new Vector3 (-1, 1, 1);
			FacingRight = false;
		}
	}
	public void Flip(){
		
		penguinBody.transform.localScale = new Vector3 ((-1 * penguinBody.transform.localScale.x), penguinBody.transform.localScale.y, penguinBody.transform.localScale.z);
		penguinBody.velocity = new Vector2 ((-1 * LastSpeed), penguinBody.velocity.y);
		if (FacingRight) {
			FacingRight = false;
		} else {
			FacingRight = true;
		}
		comboSpeed += 0.1f;
		if (comboSpeed >= maxComboSpeed) {
			comboSpeed = maxComboSpeed;
		}
		Cooldown = 3;
	}

	private void fixPos (){
		if (penguinBody.transform.position.y + penguinBody.velocity.y * Time.deltaTime >= 66) {
			
			GameObject[] floorList = GameObject.FindGameObjectsWithTag("Floor");
			GameObject[] wallLeftList = GameObject.FindGameObjectsWithTag ("WallLeft");
			GameObject[] wallRightList = GameObject.FindGameObjectsWithTag ("WallRight");
			GameObject[] icicleList = GameObject.FindGameObjectsWithTag ("Icicle");
			foreach (GameObject obj in floorList) {
				Objectlist.Add (obj);
			}
			foreach (GameObject obj in wallLeftList) {
				Objectlist.Add (obj);
			}
			foreach (GameObject obj in wallRightList) {
				Objectlist.Add (obj);
			}
			foreach (GameObject obj in icicleList) {
				Objectlist.Add (obj);
			}
			float deltaPos = -penguinBody.velocity.y * Time.deltaTime;
			foreach (GameObject obj in Objectlist) {
				obj.transform.Translate (0,deltaPos, 0);
			}
			Objectlist.Clear ();
			penguinBody.transform.position = new Vector2 (penguinBody.transform.position.x, 66);
		}
		if (penguinBody.transform.position.y < -96) {
			isAlive = false;
		}
	}


	private void reduceCooldown(){
		if(Cooldown>0){
			Cooldown--;
		}
	}

	private void animatorState(){
		anim.SetBool ("isGrounded", isGrounded ());
		anim.SetFloat ("WalkSpeed", Mathf.Abs(penguinBody.velocity.x/10));
		anim.SetFloat ("JumpSpeed", Mathf.Abs (penguinBody.velocity.y / 10));
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) {
			anim.speed = anim.GetFloat ("WalkSpeed");
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Jumping")) {
			anim.speed = anim.GetFloat ("JumpSpeed");
		}
	}
	private void ChangeAlpha(Material mat, float alphaValue)
	{
		Color oldColor = mat.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);         
		mat.SetColor("_Color", newColor);               
	}
	private int fadeState(){
		ChangeAlpha (penguinRenderer.material, alphaState);
		if (alphaState >= 1) {
			order = -1f;
			alphaState =1 + order * 0.7f * Time.fixedDeltaTime;
			return 1;
		}
		if (alphaState >= 0.5f) {
			alphaState =alphaState + order * 0.7f * Time.fixedDeltaTime;
		}
		if (alphaState < 0.5f) {
			order = 1f;
			alphaState =0.5f;
			return 1;
		}
		return 0;
	}
	public void fadePenguin(int repeat){
		if (fadeCount < repeat) {
			fadeCount += fadeState ();
		} else {
			ChangeAlpha (penguinRenderer.material, 1);
			fadeCount = 0;
			gotHit = false;
		}
	}

	public void icicleHit(){
		if (gotHit) {
			fadePenguin (7);
		}
	}

	private void gameOver(){
		if (Health == 0) {
			isAlive = false;
		}
		if (isAlive == false) {
			penguinBody.constraints = RigidbodyConstraints2D.FreezeAll;
			controlRef.fallingVel = 0;
			CC.MakeActive ();
			DBref.ShowScores ();
			if (DBref.newHighScore()&& !DBref.scoreEntered) {
				DBref.nameDialog.SetActive(true);
			}
		}
	}
	// Use this for initialization
	void Start () {
		penguinRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		CC = GameObject.Find ("CanvasObject").GetComponent<canvasController>();
		DBref = GameObject.Find ("HighScore").GetComponent<DBscript> ();
		anim = GetComponent<Animator> ();
		controlRef = GameObject.Find ("GameController").GetComponent<theController> ();
		penguinBody = this.gameObject.GetComponent<Rigidbody2D>();
		ScoreText = GameObject.Find ("ScoreView").GetComponent<GUIText> ();
		ScoreText.text = "Score: " + Score;
	}
	
	// Update is called once per frame, fixed framerate
	void FixedUpdate () {
		movement ();
		reduceCooldown();
		animatorState ();
		icicleHit ();
	}
	// Update is called once per frame, frame pushed when ready
	void Update(){
		turnCorrect ();
		fixPos ();
		gameOver ();
		getLastVelocity ();
	}
}
