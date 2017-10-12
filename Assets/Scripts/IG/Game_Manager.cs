using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

	private bool isSelectionActive = false;
	private string actualTag ;
	private int actualMatchs = 0 ;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnClickRune (Button button)
	{
		if (isSelectionActive == false)
		{
			actualTag = button.tag;
			actualMatchs += 1;
			isSelectionActive = true;
			button.interactable = false;
		}
		if (isSelectionActive == true)
		Debug.Log(actualTag);
	}
}
