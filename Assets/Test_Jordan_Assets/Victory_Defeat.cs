using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory_Defeat : MonoBehaviour {

	public GameObject enemy;
	public GameObject player;

	public float enemyHP;
	public float playerHP;
	public bool isGameEnded = false;
	public GameObject endGameVictory;

	// Use this for initialization
	void Start () {
		// enemyHP = enemy.GetComponent<Enemy_Manager>().enemyHP;
		// playerHP = player.GetComponent<Player_Manager>().playerHP;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGameEnded){
			if (enemyHP <= 0){
				// Victory
				Time.timeScale = 0;
				// endGameVictory.SetActive(true);
				isGameEnded = true;
			} else if (playerHP <= 0){
				//Defeat
				Time.timeScale = 0;
				// endGameDefeat.SetActive(true);
				isGameEnded = true;
			}
		}
		
	}
}
