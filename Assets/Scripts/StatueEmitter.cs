﻿using UnityEngine;
using System.Collections;

public class StatueEmitter : MonoBehaviour {
	public GameObject statue;

	// Use this for initialization
	void Start () {
		GameObject statueInstance = (GameObject)Instantiate (statue);
		statueInstance.gameObject.transform.position = gameObject.transform.GetChild (Random.Range (0, gameObject.transform.childCount)).transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
