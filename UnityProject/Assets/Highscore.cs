using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//special class for holding player score and rules for sorting 
public class Highscore : IComparable<Highscore> {
	
	public int ID { get; set; }
	public string Name { get; set; }
	public int EndScore { get; set; }

	public Highscore(int rank, string name, int endScore){
		this.EndScore = endScore;
		this.Name = name;
		this.ID = rank;
	}

	//sorting rules: -1 switches current to lower, +1 switches current to higher, 0 doesn't change
	public int CompareTo(Highscore other){
		if (other.EndScore < this.EndScore) {
			return -1;
		}
		else if (other.EndScore > this.EndScore) {
			return 1;
		}
		else {
			return 0;
		}
	}
}
