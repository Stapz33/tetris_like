﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Life_Manager : MonoBehaviour {

	public int lifeCount = 3;

	private bool noLife = false;
	private bool lvlUnlock = false;
	private bool returnToShop = false;

	private float timerOffline = 1200;
	private float differenceTime;
	
	private DateTime currentDate;
    private DateTime oldDate;

    private TimeSpan difference;

    private TimeSpan timeDifference1 = new TimeSpan( 0, 30, 0);
    private TimeSpan timeDifference2 = new TimeSpan( 1, 0, 0);
    private TimeSpan timeDifference3 = new TimeSpan( 1, 30, 0);

	private static Life_Manager instance ;
    public static Life_Manager Instance () 
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
		SetDateTimer();

	}
	
	// Update is called once per frame
	void Update () {
		CantPlay();
		TimerLife();
	}
	public int SendLifeCount()
	{
		return lifeCount;
	}

	public void AddLife()

	{
		if (lifeCount < 3)
		{
			lifeCount += 1;
		}
	}

	public void SubstractLife()

	{
		if (lifeCount > 0)
		{
			lifeCount -= 1;
		}
	}

	public void CantPlay()

	{
		if (lifeCount == 0)
		{
			noLife = true;
		}
		if (lifeCount > 0)
		{
			noLife = false;
		}
	}
	public bool NoLifeAsk()
	{
		return noLife;
	}

	

	public void TimerLife ()
	{
		if (lifeCount < 3)
		{
			timerOffline -= Time.deltaTime;
		}
		if (timerOffline <= 0)
		{
			AddLife();
			timerOffline = 1200;
		}
	}

	public void SetDateTimer(){
		lvlUnlock = Convert.ToBoolean(PlayerPrefs.GetInt("lvlUnlock"));
		lifeCount = PlayerPrefs.GetInt("lifeCount");
		Debug.Log(lifeCount);
		 currentDate = DateTime.Now;
 
         oldDate = Convert.ToDateTime(PlayerPrefs.GetString("currentTIme"));

         difference = currentDate.Subtract(oldDate);
         Debug.Log(difference);

         if (lifeCount == 0)
         {
         	if (difference > timeDifference1)
         	{
         		AddLife();
				timerOffline = 1200;
        	 }
        	if (difference > timeDifference2)
        	{
         		AddLife();
				timerOffline = 1200;
        	 }
        	 if (difference > timeDifference3)
        	{
         		AddLife();
				timerOffline = 1200;
        	 }
         }
         if (lifeCount == 1)
         {
         	if (difference > timeDifference1)
        	{
         		AddLife();
				timerOffline = 1200;
        	 }
        	 if (difference > timeDifference2)
        	{
         		AddLife();
				timerOffline = 1200;
        	 }
         }
         if (lifeCount == 2)
         {
         	if (difference > timeDifference1)
         	{
         		AddLife();
				timerOffline = 1200;
        	 }
         }
 
	}
	public void ResetLifes()
	{
		lifeCount = 3;
	}
	public void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("lvlUnlock",lvlUnlock ? 1 : 0);
		PlayerPrefs.SetInt("lifeCount",lifeCount);
		PlayerPrefs.SetFloat("timerOffline",timerOffline);
		PlayerPrefs.SetString("currentTIme", DateTime.Now.ToString());
	}
	public bool ReturnLVL3()
	{
		return lvlUnlock;
	}
	public void UnlockLvl()
	{
		lvlUnlock = true;
	}
	public void LockLvl()
	{
		lvlUnlock = false;
	}
	public void TrueToLifeStore()
	{
		returnToShop = true;
	}
	public void FalseToLifeStore()
	{
		returnToShop = false;
	}
	public bool ReturnLifeStore()
	{
		return returnToShop ;
	}
}
