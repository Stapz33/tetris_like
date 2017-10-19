using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {

	public GameObject boardLevels;
	public GameObject mainMenu;
	public GameObject winMenu;
	public GameObject loseMenu;
	public GameObject askTuto;
	public GameObject tutoOne;
	public GameObject tutoTwo;

	private static Menu_Manager instance ;
    public static Menu_Manager Instance () 
    {
        return instance;
    }

void Awake ()

    {
        if (instance != null)
        {
            Destroy (gameObject);
        }
        else 
        {
            instance = this;
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayOnClick(){
		mainMenu.SetActive(false);
		boardLevels.SetActive(true);
		loseMenu.SetActive(false);
		winMenu.SetActive(false);
		//renseigner le bon nom de scene
	}

	public void QuitOnClick(){
		Application.Quit();
		Debug.Log("Quit");
	}

	public void ReturnToMenu(){
		boardLevels.SetActive(false);
		mainMenu.SetActive(true);
		loseMenu.SetActive(false);
		winMenu.SetActive(false);

	}
	public void DispWin (){
		boardLevels.SetActive(false);
		mainMenu.SetActive(false);
		loseMenu.SetActive(false);
		winMenu.SetActive(true);
	}

	public void DispLose (){
		boardLevels.SetActive(false);
		mainMenu.SetActive(false);
		winMenu.SetActive(false);
		loseMenu.SetActive(true);
	}
	public void LvlOne (){
		SceneManager.LoadScene("LVL1_Scene", LoadSceneMode.Single);
	}
	public void LvlTwo (){
		SceneManager.LoadScene("LVL2_Scene", LoadSceneMode.Single);
	}
}
