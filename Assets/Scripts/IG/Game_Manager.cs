using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

	private bool isSelectionActive = false;

	private string actualTag ;
	private string shapeTag;

	public Image playerHp;
	public Image ennemyHp;
	public Image ennemyShield;

	public GameObject spawnManager;
	public GameObject[] despawns;

	private int actualMatchs = 0 ;
	public int lifeLostPerSeconds;

	public float hitEnnemyWithShield;
	public float hitEnnemyWithoutShield;
	public float ennemyShieldPerHit;
	public float ennemyShieldRecover;
	public float timerInSeconds;
	private float tmpTimer;

	public Text timer;

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
		tmpTimer = timerInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		MaxMatchs();
		CooldownPlayerHp();
		VictoryOrDefeat();
		CooldownTimer();
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
	}

	public void MaxMatchs ()

	{
		Debug.Log(actualMatchs);
		if (actualMatchs >= 3)
		{
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
			Spawn_Manager.Instance().UnSpawnForms();
			Spawn_Manager.Instance().StartingIdx();
			actualMatchs = 0;
			isSelectionActive = false;
			ResetTimer();

		}
	}

	public void PlayerHpBar ()

	{
		playerHp.fillAmount += 0.2f;
	}

	public void CooldownPlayerHp ()

	{
		playerHp.fillAmount -= Time.deltaTime / lifeLostPerSeconds;
	}

	public void EnnemyHpBar ()

	{
		if (ennemyShield.fillAmount > 0)
			{
				ennemyHp.fillAmount -= hitEnnemyWithShield;
			}
		if (ennemyShield.fillAmount == 0)
			{
				ennemyHp.fillAmount -= hitEnnemyWithoutShield;
			}
	}

	public void EnnemyShieldBar ()

	{
		ennemyShield.fillAmount -= ennemyShieldPerHit;
	}

	public void VictoryOrDefeat ()
	{
		if (playerHp.fillAmount == 0f)
		{
			SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single);
			Menu_Manager.Instance().DispLose();
		}
		if (ennemyHp.fillAmount == 0f)
		{
			SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single);
			Menu_Manager.Instance().DispWin();
		}
	}

	public void ResetTimer()  // remettre a la valeur de depart le timer, et rempli le shield

	{
		// respawn shapes ICI !
		tmpTimer = timerInSeconds;
	}

	public void CooldownTimer()

	{
		tmpTimer -= Time.deltaTime;
		timer.text = tmpTimer.ToString("F2") + "s";
		if (tmpTimer <= 0.0f)
		{
			tmpTimer = 0;
			ResetTimer();
			ennemyShield.fillAmount += ennemyShieldRecover;
		}
	}
}
