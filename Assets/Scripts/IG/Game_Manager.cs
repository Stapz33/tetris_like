using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Game_Manager : MonoBehaviour {

	private bool isSelectionActive = false;
	private bool endGame = false;
	private bool spriteChanged = false;
	private bool noLifeIG = false;
	private bool greenBoostActive = false;
	private bool blueBoostActive = false;
	private bool redBoostActive = false;

	private string actualTag ;
	private string shapeTag;

	public Image playerHp;
	public Image ennemyHp;
	public Image ennemyShield;

	public GameObject winPanel;
	public GameObject losePanel;
	public GameObject pausePanel;
	public GameObject spriteIdle;
	public GameObject spriteShielded;
	public GameObject spriteDamaged;
	public GameObject shieldFB;

	public Button greenBoost;
	public Button blueBoost;
	public Button redBoost;

	public Button greenBoostI;
	public Button blueBoostI;
	public Button redBoostI;

	private int actualMatchs = 0 ;
	public int lifeLostPerSeconds;
	private int greenBoostNb;
	private int blueBoostNb;
	private int redBoostNb;

	public float hitEnnemyWithShield;
	public float hitEnnemyWithoutShield;
	public float ennemyShieldPerHit;
	public float ennemyShieldRecover;
	public float timerInSeconds;
	private float tmpTimer;
	private float spriteTimerActual;
	public float initialSpriteTimer;

	public Text timer;
	public Text compterB;
	public Text compterR;
	public Text compterG;


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
		spriteTimerActual = initialSpriteTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if (endGame == false)
		{
		MaxMatchs();
		CooldownPlayerHp();
		VictoryOrDefeat();
		CooldownTimer();	
		CooldownSprites();
		}
		NoLifeIG();
		CheckBoostsNb();
		CheckBoostsDispo();
		CheckShieldBroken();
		Debug.Log(ennemyShield.fillAmount);
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
		if (greenBoostActive)
		{
			playerHp.fillAmount += 0.2f;
		}
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
				if (redBoostActive)
				{
					ennemyHp.fillAmount -= hitEnnemyWithShield;
				}
				ennemyHp.fillAmount -= hitEnnemyWithShield;
				spriteIdle.SetActive(false);
				spriteDamaged.SetActive(true);
				spriteChanged = true;
			}
		if (ennemyShield.fillAmount == 0)
			{
				if (redBoostActive)
				{
					ennemyHp.fillAmount -= hitEnnemyWithoutShield;
				}
				ennemyHp.fillAmount -= hitEnnemyWithoutShield;
				spriteIdle.SetActive(false);
				spriteDamaged.SetActive(true);
				spriteChanged = true;
			}
	}

	public void EnnemyShieldBar ()

	{
		if(blueBoostActive)
		{
			ennemyShield.fillAmount -= ennemyShieldPerHit;
		}
		ennemyShield.fillAmount -= ennemyShieldPerHit;
		spriteIdle.SetActive(false);
		spriteShielded.SetActive(true);
		spriteChanged = true;
	}

	public void VictoryOrDefeat ()
	{
		if (playerHp.fillAmount == 0f)
		{
			losePanel.SetActive(true);
			endGame = true;
			Life_Manager.Instance().SubstractLife();


		}
		if (ennemyHp.fillAmount == 0f)
		{
			winPanel.SetActive(true);
			endGame = true;
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
		timer.text = tmpTimer.ToString("F1") + "s";
		if (tmpTimer <= 0.0f)
		{
			tmpTimer = 0;
			ResetTimer();
			ennemyShield.fillAmount += ennemyShieldRecover;
			Spawn_Manager.Instance().UnSpawnForms();
			Spawn_Manager.Instance().StartingIdx();
			actualMatchs = 0;
			isSelectionActive = false;
		}
	}
	public void Abandonne()
	{
		Time.timeScale = 1;
		Life_Manager.Instance().SubstractLife();
		SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single);
	}
	public void ContinueToPlay()
	{
		if (!noLifeIG)
		{
			SceneManager.LoadScene("LVL2_Scene", LoadSceneMode.Single);
		}
		
	}
	public void RetryToPlay()
	{
		if (!noLifeIG)
		{
			SceneManager.LoadScene("LVL1_Scene", LoadSceneMode.Single);
		}
		
	}

	public void OnClickPause(){
		pausePanel.SetActive(true);
		Time.timeScale = 0;
	}

	public void OnClickQuitPause(){
		pausePanel.SetActive(false);
		Time.timeScale = 1;
	}
	public void CooldownSprites(){
		if (spriteChanged)
		{
			spriteTimerActual -= Time.deltaTime;
			if (spriteTimerActual <= 0)
			{
				spriteTimerActual = initialSpriteTimer;
				spriteDamaged.SetActive(false);
				spriteShielded.SetActive(false);
				spriteIdle.SetActive(true);
			}
		}
		
	}
	public void RetryToPlayLVL2()
	{
		if (!noLifeIG)
		{
			SceneManager.LoadScene("LVL2_Scene", LoadSceneMode.Single);
		}
		
	}

	public void NoLifeIG()

	{
		noLifeIG = Life_Manager.Instance().NoLifeAsk();
	}
	public void CheckBoostsNb()
	{
		redBoostNb = Boost_Manager.Instance().ReturnRed();
		blueBoostNb = Boost_Manager.Instance().ReturnBlue();
		greenBoostNb = Boost_Manager.Instance().ReturnGreen();
		compterB.text = blueBoostNb.ToString();
		compterG.text = greenBoostNb.ToString();
		compterR.text = redBoostNb.ToString();
	}

	public void UseBoostGreen()
	{
		if (greenBoostNb > 0)
		{
		greenBoostActive = true;
		Boost_Manager.Instance().UseBoostGreen();
		greenBoost.interactable = false;
		}
		
	}
	public void UseBoostBlue()
	{
		if (blueBoostNb > 0)
		{
		blueBoostActive = true;
		Boost_Manager.Instance().UseBoostBlue();
		blueBoost.interactable = false;
		}
		
	}
	public void UseBoostRed()
	{
		if (redBoostNb > 0)
		{
		redBoostActive = true;
		Boost_Manager.Instance().UseBoostRed();
		redBoost.interactable = false;
		}
		
	}
	public void CheckBoostsDispo()
	{
		if (redBoostNb == 0)
		{
			redBoost.gameObject.SetActive(false);
			redBoostI.gameObject.SetActive(true);
		}

		if (blueBoostNb == 0)
		{
			blueBoost.gameObject.SetActive(false);
			blueBoostI.gameObject.SetActive(true);
		}

		if (greenBoostNb == 0)
		{
			greenBoost.gameObject.SetActive(false);
			greenBoostI.gameObject.SetActive(true);
		}
		if (redBoostNb > 0)
		{
			redBoost.gameObject.SetActive(true);
			redBoostI.gameObject.SetActive(false);
		}

		if (blueBoostNb > 0)
		{
			blueBoost.gameObject.SetActive(true);
			blueBoostI.gameObject.SetActive(false);
		}

		if (greenBoostNb > 0)
		{
			greenBoost.gameObject.SetActive(true);
			greenBoostI.gameObject.SetActive(false);
		}
	}

	void OnDestroy()
	{
		greenBoostActive = false;
		blueBoostActive = false;
		redBoostActive = false;
	}
	public void ReturnToMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single);
	}

	public void CheckShieldBroken()
	{
		if (ennemyShield.fillAmount <= 0)
		{
			ennemyShield.fillAmount = 0;
			shieldFB.SetActive(true);
		}
		if (ennemyShield.fillAmount > 0)
		{
			shieldFB.SetActive(false);
		}
	}
}
