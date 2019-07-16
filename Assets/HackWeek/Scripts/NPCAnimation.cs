using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour {

	public int animation = 1;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetInteger ("Animation_int", animation);
		animator.SetFloat ("Speed_f", 0);
	}
}
