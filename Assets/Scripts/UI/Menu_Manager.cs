using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {

	public GameObject boardLevels;
	public GameObject mainMenu;
	public GameObject askTuto;
	public GameObject tutoOne;
	public GameObject tutoTwo;
	public GameObject acceuilPanel;

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
		//renseigner le bon nom de scene
	}

	public void QuitOnClick(){
		Application.Quit();
		Debug.Log("Quit");
	}

	public void ReturnToMenu(){
		boardLevels.SetActive(false);
		mainMenu.SetActive(true);

	}
	public void LvlOne (){
		askTuto.SetActive(true);
	}
	public void LvlTwo (){
		SceneManager.LoadScene("LVL2_Scene", LoadSceneMode.Single);
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
		SceneManager.LoadScene("LVLT_Scene", LoadSceneMode.Single);
	}
	public void EndCanvasTuto(){
		boardLevels.SetActive(true);
		mainMenu.SetActive(false);
		tutoTwo.SetActive(false);

	}
	public void AcceuilToMenu()
	{
		acceuilPanel.SetActive(false);
	}

}
