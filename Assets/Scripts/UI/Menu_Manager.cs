using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

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
	public GameObject shopPanel;
	public GameObject lifeShopPanel;
	public GameObject lfsConfirm;
	public GameObject bsConfirm;
	public GameObject splashScreen;
	public GameObject loadingScreen;


	public AudioClip clickSound, beginSound, shopBuy;
	public AudioSource audio;
	public AudioSource audioMain;
	public AudioSource audioSplash;

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
		AcceuilPassed();
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		DisplayLifes();
		NoLifes();
		LoadLifes();
	}

	public void PlayOnClick(){
		audio.clip = clickSound;
		audio.Play();
		mainMenu.SetActive(false);
		boardLevels.SetActive(true);
		//renseigner le bon nom de scene
	}

	public void QuitOnClick(){
		audio.clip = clickSound;
		audio.Play();
		acceuilPassed = false;
		Application.Quit();
		Debug.Log("Quit");
	}

	public void ReturnToMenu(){
		audio.clip = clickSound;
		audio.Play();
		boardLevels.SetActive(false);
		mainMenu.SetActive(true);

	}
	public void LvlOne (){
		audio.clip = clickSound;
		audio.Play();
		if (tutoPassed == false)
		{

			askTuto.SetActive(true);
			tutoPassed = true;
			return;
		}
		if (tutoPassed && !noLife)
		{
			loadingScreen.SetActive(true);
			StartCoroutine(LoadScreen());
		}
		
	}
	IEnumerator LoadScreen()
	{
		yield return new WaitForSeconds(3f);
		 AsyncOperation async = SceneManager.LoadSceneAsync("LVL1_Scene", LoadSceneMode.Single);

	}
	IEnumerator LoadScreen2()
	{
		yield return new WaitForSeconds(3f);
		 AsyncOperation async = SceneManager.LoadSceneAsync("LVLT_Scene", LoadSceneMode.Single);

	}
	IEnumerator LoadScreen3()
	{
		yield return new WaitForSeconds(3f);
		 AsyncOperation async = SceneManager.LoadSceneAsync("LVL2_Scene", LoadSceneMode.Single);

	}
	public void LvlTwo (){
		audio.clip = clickSound;
		audio.Play();
		if (!noLife)
		{
			loadingScreen.SetActive(true);
			StartCoroutine(LoadScreen3());
		}
		
	}
	public void YesToTuto(){
		audio.clip = clickSound;
		audio.Play();
		askTuto.SetActive(false);
		tutoOne.SetActive(true);
	}
	public void NoToTuto(){
		audio.clip = clickSound;
		audio.Play();
		askTuto.SetActive(false);
		boardLevels.SetActive(true);
	}
	public void NextTuto(){
		audio.clip = clickSound;
		audio.Play();
		tutoOne.SetActive(false);
		tutoTwo.SetActive(true);
	}
	public void EndTuto(){
		audio.clip = clickSound;
		audio.Play();
		if (!noLife)
		{
			loadingScreen.SetActive(true);
			StartCoroutine(LoadScreen2());
		}
		
	}
	public void EndCanvasTuto(){
		audio.clip = clickSound;
		audio.Play();
		boardLevels.SetActive(true);
		mainMenu.SetActive(false);
		tutoTwo.SetActive(false);

	}
	public void AcceuilToMenu()
	{
		audio.clip = clickSound;
		audio.Play();
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
			splashScreen.SetActive(false);
			audioSplash.Stop();
			audioMain.Play();
		}
	}
	public void NoLifes()
	{
		noLife = Life_Manager.Instance().NoLifeAsk();
	}
	public void CloseShop()
	{
		audio.clip = clickSound;
		audio.Play();
		shopPanel.SetActive(false);
	}
	public void OpenShop()
	{
		audio.clip = clickSound;
		audio.Play();
		shopPanel.SetActive(true);
	}
	public void OpenLifeShop()
	{
		audio.clip = clickSound;
		audio.Play();
		lifeShopPanel.SetActive(true);
	}
	public void CloseLifeShop()
	{
		audio.clip = clickSound;
		audio.Play();
		lifeShopPanel.SetActive(false);
	}
	public void BuyLife()
	{
		audio.clip = shopBuy;
		audio.Play();
		Life_Manager.Instance().ResetLifes();
		lfsConfirm.SetActive(true);
	}
	public void ConfirmLFS()
	{
		audio.clip = clickSound;
		audio.Play();
		lfsConfirm.SetActive(false);
	}
	public void BuyBoost1()
	{
		audio.clip = shopBuy;
		audio.Play();
		Boost_Manager.Instance().AddBoost1();
		bsConfirm.SetActive(true);
	}
	public void BuyBoost2()
	{
		audio.clip = shopBuy;
		audio.Play();
		Boost_Manager.Instance().AddBoost2();
		bsConfirm.SetActive(true);
	}
	public void BuyBoost3()
	{
		audio.clip = shopBuy;
		audio.Play();
		Boost_Manager.Instance().AddBoost3();
		bsConfirm.SetActive(true);
	}
	public void ConfirmBS1()
	{
		audio.clip = clickSound;
		audio.Play();
		bsConfirm.SetActive(false);
	}	
	public void SplashScreen()
	{
		splashScreen.SetActive(false);
		audioMain.Play();
	}

}
