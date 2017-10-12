using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Manager : MonoBehaviour {

	public float timerInSeconds;
	public float tmpTimer;
	public GameObject spawnSystem;
	// private Spawn_Manager spawnManager = spawnSystem.GetComponent<Spawn_Manager>();

	// Use this for initialization
	void Start () {
		Spawn_Manager spawnManager = spawnSystem.GetComponent<Spawn_Manager>();
		tmpTimer = timerInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		tmpTimer -= Time.deltaTime;
		if (tmpTimer < 0){
			ResetTimer();
		}
	}

	public void ResetTimer(){
		// spawnManager.SpawnForm();
		tmpTimer = timerInSeconds;
	}
}
