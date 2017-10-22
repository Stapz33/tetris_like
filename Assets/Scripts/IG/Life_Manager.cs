using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Manager : MonoBehaviour {

	private static int lifeCount = 3;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
		if (lifeCount > 1)
		{
			lifeCount -= 1;
		}
	}
}
