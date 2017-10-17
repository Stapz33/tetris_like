using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield_Manager : MonoBehaviour {

	public Image shield;
	public float shieldRefill;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (shield.fillAmount < 1.0f){
			if (Input.GetKeyDown(KeyCode.E)){
				RefillShield();
			}
			
		}
	}

	public void RefillShield(){
		if((shield.fillAmount + shieldRefill) <= 1.0f){
			shield.fillAmount += shieldRefill;
		} else if ((shield.fillAmount + shieldRefill) > 1.0f){
			shield.fillAmount = 1.0f;
		}
	}
}
