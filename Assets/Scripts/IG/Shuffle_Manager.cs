using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle_Manager : MonoBehaviour {

	public GameObject[] slots;

	public List<GameObject> tmpSlots = new List<GameObject>();


	void Start () {
		ShuffleManager();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ShuffleManager(){
		for (int i = 0; i < 9; i++){
			tmpSlots.Add(slots[0]);
		}
		for (int y = 0; y < 9; y++){
			int tmp = Random.Range(0, 9);
			if (tmpSlots[tmp] !=null){
				slots[y] = tmpSlots[tmp];
				tmpSlots[tmp]=null;
			} else {
				y--;
			}
		}
		tmpSlots = null;
	}
}
