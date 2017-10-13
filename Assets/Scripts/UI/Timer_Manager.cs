using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Manager : MonoBehaviour {

	public float timerInSeconds;
	public float tmpTimer;
	// public GameObject spawnSystem;
	public Text timer;
	public float refillShield; //portion de shield a remplir
	public GameObject shieldEnemy;
	public float actualShield; // pourcentage de shield enemy actuel
	public float maxShield; // maximum du shield enemy
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

	public void ResetTimer(){ // remettre a la valeur de depart le timer, et rempli le shield
		// spawnSystem.GetComponent<Spawn_Manager>().SpawnForm();;
		Debug.Log("Reset");
		RefillShield();
		tmpTimer = timerInSeconds;
	}

	public void RefillShield(){ // remplissage du shield enemy a la fin du timer
		if (actualShield == maxShield){ // si le shield est au maximum, il ne se passe rien
			return;
		} else if (actualShield < maxShield){
			if (actualShield + refillShield >= maxShield){ // si le pourcentage a remplir depasse le max, on bloque au max
				actualShield = maxShield;
			} else { // rempli le shield
				actualShield += refillShield;
			}
		}
	}
}
