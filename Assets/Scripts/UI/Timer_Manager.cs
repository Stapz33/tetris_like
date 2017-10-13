using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Manager : MonoBehaviour {

	public float timerInSeconds;
	public float tmpTimer;
	// public GameObject spawnSystem;
	public Text timer;
	// private Spawn_Manager spawnManager = spawnSystem.GetComponent<Spawn_Manager>();

	// Use this for initialization
	void Start () {
		// Spawn_Manager spawnManager = spawnSystem.GetComponent<Spawn_Manager>();
		tmpTimer = timerInSeconds;
		// timer = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		tmpTimer -= Time.deltaTime;
		timer.text = tmpTimer.ToString("F2")+" s";
		if (tmpTimer <= 0.0f){
			ResetTimer();
		}
	}

	public void ResetTimer(){
		// spawnSystem.GetComponent<Spawn_Manager>().SpawnForm();;
		Debug.Log("Reset");
		tmpTimer = timerInSeconds;
	}
}
