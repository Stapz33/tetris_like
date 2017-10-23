using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu_Manager : MonoBehaviour {

	public GameObject boardLevels;
	public GameObject mainMenu;
	public GameObject askTuto;
	public GameObject tutoOne;
	public GameObject tutoTwo;
	public GameObject acceuilPanel;
	public GameObject lifeOne;
	public GameObject lifeTwo;
	public GameObject lifeThree;

	private static bool tutoPassed = false;
	private static bool acceuilPassed = false;
	private bool noLife = false;

	private int lifeCount = 3;

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
		LoadLifes();
		AcceuilPassed();
	}
	
	// Update is called once per frame
	void Update () {
		DisplayLifes();
		NoLifes();
	}

	public void PlayOnClick(){
		mainMenu.SetActive(false);
		boardLevels.SetActive(true);
		//renseigner le bon nom de scene
	}

	public void QuitOnClick(){
		acceuilPassed = false;
		Application.Quit();
		Debug.Log("Quit");
	}

	public void ReturnToMenu(){
		boardLevels.SetActive(false);
		mainMenu.SetActive(true);

	}
	public void LvlOne (){
		if (tutoPassed == false)
		{
			askTuto.SetActive(true);
			tutoPassed = true;
			return;
		}
		if (tutoPassed && !noLife)
		{
			SceneManager.LoadScene("LVL1_Scene", LoadSceneMode.Single);
		}
		
	}
	public void LvlTwo (){
		if (!noLife)
		{
			SceneManager.LoadScene("LVL2_Scene", LoadSceneMode.Single);
		}
		
	}
	public void YesToTuto(){
		askTuto.SetActive(false);
		tutoOne.SetActive(true);
	}
	public void NoToTuto(){
		askTuto.SetActive(false);
		boardLevels.SetActive(true);
	}
	public void NextTuto(){
		tutoOne.SetActive(false);
		tutoTwo.SetActive(true);
	}
	public void EndTuto(){
		if (!noLife)
		{
			SceneManager.LoadScene("LVLT_Scene", LoadSceneMode.Single);
		}
		
	}
	public void EndCanvasTuto(){
		boardLevels.SetActive(true);
		mainMenu.SetActive(false);
		tutoTwo.SetActive(false);

	}
	public void AcceuilToMenu()
	{
			acceuilPanel.SetActive(false);
			acceuilPassed = true;	
	}

	public void DisplayLifes()

	{
		if (lifeCount == 3)
		{
			lifeThree.SetActive(true);
			lifeOne.SetActive(true);
			lifeTwo.SetActive(true);
		}
		if (lifeCount == 2)
		{
			lifeThree.SetActive(false);
			lifeTwo.SetActive(true);
			lifeOne.SetActive(true);
		}
		if (lifeCount == 1)
		{
			lifeThree.SetActive(false);
			lifeTwo.SetActive(false);
			lifeOne.SetActive(true);
		}
		if (lifeCount == 0)
		{
			lifeThree.SetActive(false);
			lifeTwo.SetActive(false);
			lifeOne.SetActive(false);
		}
	}

	public void LoadLifes()
	{
		lifeCount = Life_Manager.Instance().SendLifeCount();
	}
	public void AcceuilPassed(){
		if (acceuilPassed)
		{
			acceuilPanel.SetActive(false);
		}
	}
	public void NoLifes()
	{
		noLife = Life_Manager.Instance().NoLifeAsk();
	}
	public void OnApplicationQuit()
	{
		Life_Manager.Instance().SaveTimer();
	}
}
