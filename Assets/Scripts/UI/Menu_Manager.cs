using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayOnClick(){
		SceneManager.LoadScene("Killian_Test");
		//renseigner le bon nom de scene
	}

	public void QuitOnClick(){
		Application.Quit();
		Debug.Log("Quit");
	}
}
