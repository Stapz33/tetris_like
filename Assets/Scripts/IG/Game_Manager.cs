using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

	private bool isSelectionActive = false;
	private string actualTag ;
	private int actualMatchs = 0 ;
	public GameObject mus;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		MaxMatchs();
		Debug.Log(actualMatchs);
	}
	public void OnClickRune (Button button)
	{
		if (isSelectionActive == true && actualMatchs < 3)
		{
			if (actualTag == button.tag)
			{
				actualMatchs += 1;
				button.interactable = false;
			}

		}
		if (isSelectionActive == false && actualMatchs == 0)
		{
			actualTag = button.tag;
			actualMatchs += 1;
			isSelectionActive = true;
			button.interactable = false;
		}
		Debug.Log(actualTag);
	}
	public void MaxMatchs ()
	{
		if (actualMatchs >= 3)
		{
			mus.SetActive(false);
		}
	}
}
