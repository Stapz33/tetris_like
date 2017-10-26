using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen_Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SplashStopAnim()
	{
		Menu_Manager.Instance().SplashScreen();
	}
}
