using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	private MovePenguin mp;
	private SpriteRenderer[] heartList;
	private Sprite Full;
	private Sprite Half;
	private Sprite Empty;

	private void SetHearts(int hp){
		if (hp == 6) {
			heartList [0].sprite = Full;
			heartList [1].sprite = Full;
			heartList [2].sprite = Full;
		}
		if (hp == 5) {
			heartList [0].sprite = Half;
			heartList [1].sprite = Full;
			heartList [2].sprite = Full;
		}
		if (hp == 4) {
			heartList [0].sprite = Empty;
			heartList [1].sprite = Full;
			heartList [2].sprite = Full;
		}
		if (hp == 3) {
			heartList [0].sprite = Empty;
			heartList [1].sprite = Half;
			heartList [2].sprite = Full;
		}
		if (hp == 2) {
			heartList [0].sprite = Empty;
			heartList [1].sprite = Empty;
			heartList [2].sprite = Full;
		}
		if (hp == 1) {
			heartList [0].sprite = Empty;
			heartList [1].sprite = Empty;
			heartList [2].sprite = Half;
		}
		if (hp == 0) {
			heartList [0].sprite = Empty;
			heartList [1].sprite = Empty;
			heartList [2].sprite = Empty;
		}
	}

	void Start () {
		mp = GameObject.Find ("Penguin").GetComponent<MovePenguin> ();
		heartList = this.gameObject.GetComponentsInChildren<SpriteRenderer> ();
		Full = Resources.Load ("Sprites/HeartFull", typeof(Sprite)) as Sprite;
		Half = Resources.Load ("Sprites/HeartHalf", typeof(Sprite)) as Sprite;
		Empty = Resources.Load ("Sprites/HeartEmpty", typeof(Sprite)) as Sprite;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SetHearts (mp.Health);
	}
}
