using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//control of visual state of penguins health
public class HealthController : MonoBehaviour {

	private MovePenguin mp;
	private SpriteRenderer[] heartList;
	private Sprite full;
	private Sprite half;
	private Sprite empty;

	private void SetHearts(int hp){
		if (hp == 6) {
			heartList [0].sprite = full;
			heartList [1].sprite = full;
			heartList [2].sprite = full;
		}
		if (hp == 5) {
			heartList [0].sprite = half;
			heartList [1].sprite = full;
			heartList [2].sprite = full;
		}
		if (hp == 4) {
			heartList [0].sprite = empty;
			heartList [1].sprite = full;
			heartList [2].sprite = full;
		}
		if (hp == 3) {
			heartList [0].sprite = empty;
			heartList [1].sprite = half;
			heartList [2].sprite = full;
		}
		if (hp == 2) {
			heartList [0].sprite = empty;
			heartList [1].sprite = empty;
			heartList [2].sprite = full;
		}
		if (hp == 1) {
			heartList [0].sprite = empty;
			heartList [1].sprite = empty;
			heartList [2].sprite = half;
		}
		if (hp == 0) {
			heartList [0].sprite = empty;
			heartList [1].sprite = empty;
			heartList [2].sprite = empty;
		}
	}

	void Start () {
		mp = GameObject.Find ("Penguin").GetComponent<MovePenguin> ();
		heartList = this.gameObject.GetComponentsInChildren<SpriteRenderer> ();
		full = Resources.Load ("Sprites/HeartFull", typeof(Sprite)) as Sprite;
		half = Resources.Load ("Sprites/HeartHalf", typeof(Sprite)) as Sprite;
		empty = Resources.Load ("Sprites/HeartEmpty", typeof(Sprite)) as Sprite;
	}
	

	void FixedUpdate () {
		SetHearts (mp.health);
	}
}
