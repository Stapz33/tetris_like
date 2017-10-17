using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

	private bool isSelectionActive = false;
	private string actualTag ;
	private int actualMatchs = 0 ;
	public GameObject mus;
	private string shapeTag;
	public GameObject spawnManager;
	public Image playerHp;
	public Image ennemyHp;
	public Image ennemyShield;
	public GameObject loseImage;
	public int lifeLostPerSeconds;
	private static Game_Manager instance ;
    public static Game_Manager Instance () 
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
		Debug.Log(actualTag);
		MaxMatchs();
		CooldownPlayerHp();
	}

	public void OnClickRune (GameObject shapeInUse)

	{
		shapeTag = shapeInUse.tag;
		if (isSelectionActive && actualMatchs < 3)
		{
			if (actualTag == shapeTag)
			{
				shapeInUse.GetComponent<Button>().interactable = false;
				actualMatchs += 1;
			}

		}
		if (!isSelectionActive && actualMatchs == 0)
		{
			actualTag = shapeTag;
			actualMatchs += 1;
			isSelectionActive = true;
			shapeInUse.GetComponent<Button>().interactable = false;
		}
		Debug.Log(actualTag);
	}

	public void MaxMatchs ()

	{
		if (actualMatchs >= 3)
		{
			mus.SetActive(false);
			if (actualTag == "Cube")
			{
				PlayerHpBar();
				actualTag = null;
			}
			if (actualTag == "Triangle")
			{
				EnnemyHpBar();
				actualTag = null;
			}
			if (actualTag == "Circle")
			{
				EnnemyShieldBar();
				actualTag = null;
			}

		}
	}

	public void PlayerHpBar ()

	{
		playerHp.fillAmount += 0.2f;
	}

	public void CooldownPlayerHp ()

	{
		playerHp.fillAmount -= Time.deltaTime / lifeLostPerSeconds;
		if (playerHp.fillAmount == 0f)
		{
			loseImage.SetActive(true);
		}
	}

	public void EnnemyHpBar ()

	{
		if (ennemyShield.fillAmount > 0)
			{
				ennemyHp.fillAmount -= 0.1f;
			}
		if (ennemyShield.fillAmount == 0)
			{
				ennemyHp.fillAmount -= 0.5f;
			}
	}

	public void EnnemyShieldBar ()

	{
		ennemyShield.fillAmount -= 0.2f;
	}
}
