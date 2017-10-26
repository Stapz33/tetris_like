using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boost_Manager : MonoBehaviour {

	public int greenBoostNb;
	public int blueBoostNb;
	public int redBoostNb;

	private static Boost_Manager instance ;
    public static Boost_Manager Instance () 
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
		DontDestroyOnLoad(gameObject);
		LoadPrefs();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public int ReturnGreen()
	{
		return greenBoostNb;
	}

	public int ReturnBlue()
	{
		return blueBoostNb;
	}

	public int ReturnRed()
	{
		return redBoostNb;
	}

	public void AddBoost1()
	{
		greenBoostNb += 1;
		redBoostNb += 1;
		blueBoostNb += 1;
	}

	public void AddBoost2()
	{
		greenBoostNb += 5;
		redBoostNb += 5;
		blueBoostNb += 5;
	}

	public void AddBoost3()
	{
		greenBoostNb += 10;
		redBoostNb += 10;
		blueBoostNb += 10;
	}
	public void UseBoostGreen()
	{
		greenBoostNb -= 1;
	}
	public void UseBoostBlue()
	{
		blueBoostNb -= 1;
	}
	public void UseBoostRed()
	{
		redBoostNb -= 1;
	}
	public void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("redBoostNb",redBoostNb);
		PlayerPrefs.SetInt("blueBoostNb",blueBoostNb);
		PlayerPrefs.SetInt("greenBoostNb", greenBoostNb);
	}
	public void LoadPrefs()
	{
		redBoostNb = PlayerPrefs.GetInt("redBoostNb");
		greenBoostNb = PlayerPrefs.GetInt("greenBoostNb");
		blueBoostNb = PlayerPrefs.GetInt("blueBoostNb");
	}


}
