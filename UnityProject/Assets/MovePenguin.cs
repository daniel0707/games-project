using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for controlling the Penguin
public class MovePenguin : MonoBehaviour {

	//references 
	private SpriteRenderer penguinRenderer;
	private CanvasController CC;
	private DBscript DBref;
	private Animator anim;
	private TheController controlRef;
	private Rigidbody2D penguinBody;

	//variables for operating the penguin
	private bool bouncedWall;
	public bool isAlive = true;
	[HideInInspector]public bool firstJump = false;
	[HideInInspector]public bool gotHit;
	[HideInInspector]public int fadeCount = 0;
	[HideInInspector]public float alphaState = 1f;
	[HideInInspector]public float order = 0;
	[HideInInspector]public int health = 6;
	[HideInInspector]public int cooldown = 0;
	[HideInInspector]public float lastSpeed;
	[HideInInspector]public float currentSpeed;
	[HideInInspector]public bool facingRight = true;
	[HideInInspector]public GUIText scoreText;

	//variables for score calculations
	private float currentLevel = 0f;
	private float highestLevel = 0f;
	private float previousLevel = 0f;
	public float score = 0f;

	//variables for movement control
	private float jumpSpeed = 200f;
	private float comboSpeed = 1.0f;
	private int comboScore = 0;
	private float maxComboSpeed = 2.0f;
	private float maxSpeed = 100f;
	private float minSpeed = -100f;
	private float sideSpeed = 10f;
	[HideInInspector]public List<GameObject> objectlist= new List<GameObject>();

	//ground check variables
	private bool grounded = false;
	public LayerMask whatIsGround;
	public Transform pointA;
	public Transform pointB;

	public void GetLastVelocity(){
		lastSpeed = currentSpeed;
		currentSpeed = penguinBody.velocity.x;
	}

	//return true if the rectangular area between two points overlaps with layers in the mask
	//checks if the penguin is on a platform
	public bool IsGrounded(){
		grounded = Physics2D.OverlapArea (pointA.position,pointB.position, whatIsGround);
		return grounded;
	}
		
	void OnCollisionEnter2D(Collision2D other){
		
		//if penguin collides with floor we make the penguin follow floors movement
		if (other.gameObject.tag == "Floor") {
			transform.parent = other.transform;

			//add to score if player reached a higher platform
			//reward for multiple platform jump with score and movement combos
			if (IsGrounded ()) {
				BlockProperty bP = other.gameObject.GetComponent<BlockProperty> () as BlockProperty;
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
					score += (10 + comboScore) * (currentLevel - previousLevel);
				}
				if (currentLevel > previousLevel) {
					previousLevel = currentLevel;
				}
				scoreText.text = "Score: " + score;
			}
		}

		//on collision with wall do a flip and play sound
		if (other.gameObject.tag == "WallLeft" || other.gameObject.tag == "WallRight") {
			bouncedWall = true;
			other.gameObject.GetComponent<AudioSource>().Play();
			Flip ();
		}
	}

	//when leaving a platform remove penguin from being a child of the platform
	//extra measures implemented in BottomDetection class to counter this event not triggering
	void OncollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Floor") {
			transform.parent = null;
		}
	}
		
	//return true if penguins velocity is close to 0
	//prevents jumping when inside platform
	private bool isFalling(){
		if (penguinBody.velocity.y <= 0.1f) {
			return true;
		} else {
			return false;
		}
	}

	//main movement method
	private void movement(){

		//update variables
		maxSpeed = 100f * comboSpeed;
		minSpeed = -100f * comboSpeed;
		sideSpeed = 10f * comboSpeed;

		//jump only when grounded, falling and players press key UP
		//world starts moving after first jump
		if ((Input.GetKey (KeyCode.UpArrow)) && (IsGrounded()) && (isFalling())) {
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
			
		//move left or right when cooldown from bouncing the wall is over
		if (Input.GetKey (KeyCode.LeftArrow)&&cooldown == 0) {
			if (penguinBody.velocity.x > minSpeed) {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x - sideSpeed, penguinBody.velocity.y);
			} else {
				penguinBody.velocity = new Vector2 (minSpeed, penguinBody.velocity.y);
			}
		}
				
		if (Input.GetKey (KeyCode.RightArrow) && cooldown == 0) {

			if (penguinBody.velocity.x < maxSpeed) {
				penguinBody.velocity = new Vector2 (penguinBody.velocity.x + sideSpeed, penguinBody.velocity.y);
			} else {
				penguinBody.velocity = new Vector2 (maxSpeed, penguinBody.velocity.y);
			}
		}
	}//close movement method

	//update penguin localscale depending on velocity
	public void TurnCorrect (){
		if (penguinBody.velocity.x > 0) {
			facingRight = true;
			penguinBody.transform.localScale = new Vector3 (1, 1, 1);
		}
		if (penguinBody.velocity.x < 0) {
			penguinBody.transform.localScale = new Vector3 (-1, 1, 1);
			facingRight = false;
		}
	}

	//change penguin localscale, direction of velocity and speed combo
	//cooldown prevents player from slowing down due to not reacting to change of direction fast enough
	public void Flip(){
		penguinBody.transform.localScale = new Vector3 ((-1 * penguinBody.transform.localScale.x), penguinBody.transform.localScale.y, penguinBody.transform.localScale.z);
		penguinBody.velocity = new Vector2 ((-1 * lastSpeed), penguinBody.velocity.y);
		if (facingRight) {
			facingRight = false;
		} else {
			facingRight = true;
		}
		comboSpeed += 0.1f;
		if (comboSpeed >= maxComboSpeed) {
			comboSpeed = maxComboSpeed;
		}
		cooldown = 3;
	}

	//global check on penguins position
	//when penguin is too high he will be stopped and the world will scroll faster
	//upon falling off screen penguin is dead
	private void fixPos (){
		if (penguinBody.transform.position.y + penguinBody.velocity.y * Time.deltaTime >= 60) {
			GameObject[] floorList = GameObject.FindGameObjectsWithTag("Floor");
			GameObject[] wallLeftList = GameObject.FindGameObjectsWithTag ("WallLeft");
			GameObject[] wallRightList = GameObject.FindGameObjectsWithTag ("WallRight");
			GameObject[] icicleList = GameObject.FindGameObjectsWithTag ("Icicle");
			foreach (GameObject obj in floorList) {
				objectlist.Add (obj);
			}
			foreach (GameObject obj in wallLeftList) {
				objectlist.Add (obj);
			}
			foreach (GameObject obj in wallRightList) {
				objectlist.Add (obj);
			}
			foreach (GameObject obj in icicleList) {
				objectlist.Add (obj);
			}
			float deltaPos = -penguinBody.velocity.y * Time.deltaTime;
			foreach (GameObject obj in objectlist) {
				obj.transform.Translate (0,deltaPos, 0);
			}
			objectlist.Clear ();
			penguinBody.transform.position = new Vector2 (penguinBody.transform.position.x, 60);
		}
		if (penguinBody.transform.position.y < -96) {
			isAlive = false;
		}
	}

	//reduce flip cooldown each frame 
	private void reduceCooldown(){
		if(cooldown>0){
			cooldown--;
		}
	}

	//controll of the animation speed
	private void animatorState(){
		anim.SetBool ("IsGrounded", IsGrounded ());
		anim.SetFloat ("WalkSpeed", Mathf.Abs(penguinBody.velocity.x/10));
		anim.SetFloat ("JumpSpeed", Mathf.Abs (penguinBody.velocity.y / 10));
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) {
			anim.speed = anim.GetFloat ("WalkSpeed");
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Jumping")) {
			anim.speed = anim.GetFloat ("JumpSpeed");
		}
	}

	//Group of methods controlling penguin renderer alpha state
	//alpha state cycles upon receiving damage to the penguin
	private void ChangeAlpha(Material mat, float alphaValue){
		Color oldColor = mat.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);         
		mat.SetColor("_Color", newColor);               
	}

	private int fadeState(){
		ChangeAlpha (penguinRenderer.material, alphaState);
		if (alphaState >= 1) {
			order = -1f;
			alphaState =1 + order * 1.5f * Time.fixedDeltaTime;
			return 1;
		}
		if (alphaState >= 0.3f) {
			alphaState =alphaState + order * 1.5f * Time.fixedDeltaTime;
		}
		if (alphaState < 0.3f) {
			order = 1f;
			alphaState =0.3f;
			return 1;
		}
		return 0;
	}

	public void FadePenguin(int repeat){
		if (fadeCount < repeat) {
			fadeCount += fadeState ();
		} else {
			ChangeAlpha (penguinRenderer.material, 1);
			fadeCount = 0;
			gotHit = false;
		}
	}
		
	public void IcicleHit(){
		if (gotHit) {
			FadePenguin (7);
		}
	}
		
	private void gameOver(){
		if (health == 0) {
			isAlive = false;
		}
		if (isAlive == false) {
			penguinBody.constraints = RigidbodyConstraints2D.FreezeAll;
			controlRef.fallingVel = 0;
			CC.MakeActive ();
			DBref.ShowScores ();
			if (DBref.NewHighScore()&& !DBref.scoreEntered) {
				DBref.nameDialog.SetActive(true);
			}
		}
	}

	// Used for initialization
	void Start () {
		penguinRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		CC = GameObject.Find ("CanvasObject").GetComponent<CanvasController>();
		DBref = GameObject.Find ("HighScore").GetComponent<DBscript> ();
		anim = GetComponent<Animator> ();
		controlRef = GameObject.Find ("GameController").GetComponent<TheController> ();
		penguinBody = this.gameObject.GetComponent<Rigidbody2D>();
		scoreText = GameObject.Find ("ScoreView").GetComponent<GUIText> ();
		scoreText.text = "Score: " + score;
	}
	
	// Update is called once per frame, fixed framerate
	void FixedUpdate () {
		movement ();
		reduceCooldown();
		animatorState ();
		IcicleHit ();
	}

	// Update is called once per frame, frame pushed when ready
	void Update(){
		TurnCorrect ();
		fixPos ();
		gameOver ();
		GetLastVelocity ();
	}
}
