using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour {

	public Transform spawnParent;
	private List<int> spawnIdx = new List<int>();
	public GameObject circle;
	public GameObject cube;
	public GameObject triangle;
	private int circleNumber = 0;
	private int cubeNumber = 0;
	private int triangleNumber = 0;
	private int idx;
	public int gridSize;
	private static Spawn_Manager instance ;
    public static Spawn_Manager Instance () 
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
		StartingIdx();
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void RndIdx ()
	{
			idx = Random.Range(0,spawnIdx.Count);	
	}
	public void SpawnForms ()
	{
		if (circleNumber < 3)
		{
		Instantiate(circle,spawnParent.GetChild(spawnIdx[idx]).position,Quaternion.identity,spawnParent.GetChild(spawnIdx[idx]));
		circleNumber += 1;
		spawnIdx.Remove(spawnIdx[idx]);
		return;
		}
		if (cubeNumber < 3)
		{
		Instantiate(cube,spawnParent.GetChild(spawnIdx[idx]).position,Quaternion.identity,spawnParent.GetChild(spawnIdx[idx]));
		cubeNumber += 1;
		spawnIdx.Remove(spawnIdx[idx]);
		return;
		}
		if (triangleNumber < 3)
		{
		Instantiate(triangle,spawnParent.GetChild(spawnIdx[idx]).position,Quaternion.identity,spawnParent.GetChild(spawnIdx[idx]));
		triangleNumber += 1;
		spawnIdx.Remove(spawnIdx[idx]);
		return;
		}
	}
	public void ListEntry ()
	{
		spawnIdx.Clear();
		for (int i = 0; i < gridSize*gridSize ; i++)
		{
			spawnIdx.Add(i);
		}
	}

	public void StartingIdx ()

	{
			ResetNumbers();
			ListEntry();	
			for (int i = 0; i < gridSize*gridSize ; i++)
			{
				RndIdx();
				SpawnForms();
			}
	}

	public void UnSpawnForms ()

	{
		for (int i = 0; i < gridSize*gridSize ; i++)
		{
		  Destroy(spawnParent.GetChild(i).GetChild(0).gameObject);
		}
	}

	public void ResetNumbers ()

	{
		circleNumber = 0;
		cubeNumber = 0;
		triangleNumber = 0;
	}
}
