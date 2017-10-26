using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tuto_Manager : MonoBehaviour {

	private bool isSelectionActive = false;
	private bool endGame = false;
	private bool hpBool = false;
	private bool pHpBool = false;
	private bool shieldBool1 = false;
	private bool ennemyHpBool1 = false;
	private bool ennemyHpBool2 = false;
	private bool timerActive = true;
	private bool spriteChanged = false;

	private string actualTag ;
	private string shapeTag;

	public Image playerHp;
	public Image ennemyHp;
	public Image ennemyShield;
	public Image timerImage;

	public GameObject winPanel;
	public GameObject tutoHpPanel1;
	public GameObject tutoHpPanel2;
	public GameObject tutoBravo2;
	public GameObject tutoBravo3;
	public GameObject tutoBravo1;
	public GameObject tutoEnnemyPanel1;
	public GameObject tutoShieldPanel1;
	public GameObject tutoEnnemyPanel2;
	public GameObject tutoShieldPanel2;
	public GameObject tutoFinalEnnemy;
	public GameObject tutoTimer;
	public GameObject tutoFinalPanel;
	public GameObject spriteIdle;
	public GameObject spriteShielded;
	public GameObject spriteDamaged;
	public GameObject shieldFB;

	public AudioClip gemSound, shieldSound, lifeSound, damageSound, shieldBSound, victorySound, defeatSound, clickSound;
	public AudioSource audio;
	public AudioSource audiomanag;

	private int actualMatchs = 0 ;
	public int lifeLostPerSeconds;
	private int actualCombinaisons;

	public float hitEnnemyWithShield;
	public float hitEnnemyWithoutShield;
	public float ennemyShieldPerHit;
	public float ennemyShieldRecover;
	public float timerInSeconds;
	private float tmpTimer;
	private float spriteTimerActual;
	public float initialSpriteTimer;

	public Text timer;

	private static Tuto_Manager instance ;
    public static Tuto_Manager Instance () 
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
		TutoUpdate();
		MaxMatchs();
		CooldownPlayerHp();
		VictoryOrDefeat();
		CooldownTimer();
		CooldownSprites();	
		}
	}

	public void OnClickRune (GameObject shapeInUse)

	{
		shapeTag = shapeInUse.tag;
		if (isSelectionActive && actualMatchs < 3)
		{
			audio.clip = gemSound;
		audio.Play();
			if (actualTag == shapeTag)
			{
				shapeInUse.GetComponent<Button>().interactable = false;
				actualMatchs += 1;
			}

		}
		if (!isSelectionActive && actualMatchs == 0)
		{
			audio.clip = gemSound;
		audio.Play();
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
				if (pHpBool)
				{
					audio.clip = lifeSound;
		audio.Play();
				PlayerHpBar();
				actualTag = null;
				}
			}
			if (actualTag == "Triangle")
			{
				if (ennemyHpBool1 || ennemyHpBool2)
				{
					audio.clip = damageSound;
		audio.Play();
					EnnemyHpBar();
					actualTag = null;
				}
			}
			if (actualTag == "Circle")
			{
				if (shieldBool1)
				{
				EnnemyShieldBar();
				actualTag = null;
				}
				
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
		playerHp.fillAmount += 0.5f;
	}

	public void CooldownPlayerHp ()

	{
		if (!hpBool)
		{
			playerHp.fillAmount -= Time.deltaTime / lifeLostPerSeconds;
		}
		
	}

	public void EnnemyHpBar ()

	{
		if (ennemyShield.fillAmount > 0)
			{
				ennemyHp.fillAmount -= hitEnnemyWithShield;
				spriteIdle.SetActive(false);
				spriteDamaged.SetActive(true);
				spriteChanged = true;
			}
		if (ennemyShield.fillAmount == 0)
			{
				ennemyHp.fillAmount -= hitEnnemyWithoutShield;
				spriteIdle.SetActive(false);
				spriteDamaged.SetActive(true);
				spriteChanged = true;
			}
	}

	public void EnnemyShieldBar ()

	{
		ennemyShield.fillAmount -= ennemyShieldPerHit;
		CheckShieldBroken();
		spriteIdle.SetActive(false);
		spriteShielded.SetActive(true);
		spriteChanged = true;
	}

	public void VictoryOrDefeat ()
	{
		if (ennemyHp.fillAmount == 0f)
		{
			winPanel.SetActive(true);
			endGame = true;
			audiomanag.Stop();
			audio.clip = victorySound;
		audio.Play();
		}
	}

	public void ResetTimer()  // remettre a la valeur de depart le timer, et rempli le shield

	{
		// respawn shapes ICI !
		tmpTimer = timerInSeconds;
	}

	public void CooldownTimer()

	{
		if (timerActive)
		{
		tmpTimer -= Time.deltaTime;
		timerImage.fillAmount = tmpTimer / timerInSeconds;
		if (tmpTimer <= 0.0f)
		{
			tmpTimer = 0;
			ResetTimer();
			ennemyShield.fillAmount += ennemyShieldRecover;
			Spawn_Manager.Instance().UnSpawnForms();
			Spawn_Manager.Instance().StartingIdx();
		}
		}
	}
	public void ReturnToMenu()
	{
		audio.clip = clickSound;
		audio.Play();
		SceneManager.LoadScene("Menu_Scene", LoadSceneMode.Single);
	}
	public void ContinueToPlay()
	{
		audio.clip = clickSound;
		audio.Play();
		SceneManager.LoadScene("LVL2_Scene", LoadSceneMode.Single);
	}
	public void RetryToPlay()
	{
		audio.clip = clickSound;
		audio.Play();
		SceneManager.LoadScene("LVL1_Scene", LoadSceneMode.Single);
	}
	public void TutoUpdate()
	{
		if (playerHp.fillAmount <= 0.9 && !pHpBool)
		{
			hpBool = true;
			tutoHpPanel1.SetActive(true);
			pHpBool = true;
			timerActive = false;
		}
		if (playerHp.fillAmount >= 1 && pHpBool && hpBool)
		{
			tutoBravo1.SetActive(true);
			pHpBool = false;
			timerActive = false;
		}
		if (shieldBool1 && ennemyShield.fillAmount <= 0)
		{
			tutoBravo2.SetActive(true);
			shieldBool1 = false;
			timerActive = false;
		}
		if (ennemyHpBool1 && ennemyHp.fillAmount <= 0.8)
		{
			tutoBravo3.SetActive(true);
			ennemyHpBool1 = false;
			timerActive = false;
			ennemyHpBool2 = true;
		}
	}
	public void TutoHp1()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoHpPanel1.SetActive(false);
		tutoHpPanel2.SetActive(true);
	}

	public void TutoHp2()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoHpPanel2.SetActive(false);
		timerActive = true;
	}
	public void TutoBravo1()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoBravo1.SetActive(false);
		tutoShieldPanel1.SetActive(true);
	}

	public void TutoShield1()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoShieldPanel1.SetActive(false);
		tutoShieldPanel2.SetActive(true);
	}
	public void TutoShield2()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoShieldPanel2.SetActive(false);
		shieldBool1 = true;
		timerActive = true;
	}
	public void TutoBravo2()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoBravo2.SetActive(false);
		tutoEnnemyPanel1.SetActive(true);
	}
	public void TutoEnnemy1()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoEnnemyPanel1.SetActive(false);
		tutoEnnemyPanel2.SetActive(true);
	}
	public void TutoEnnemy2()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoEnnemyPanel2.SetActive(false);
		ennemyHpBool1 = true;
		timerActive = true;
	}
	public void TutoBravo3()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoBravo3.SetActive(false);
		tutoFinalEnnemy.SetActive(true);
	}
	public void TutoTimer()
	{
		audio.clip = clickSound;
		audio.Play();
		tutoFinalEnnemy.SetActive(false);
		tutoTimer.SetActive(true);
		timerActive = true;
	}
	public void TutoFinal()
	{
		audio.clip = clickSound;
		audio.Play();
		timerActive = false;
		tutoTimer.SetActive(false);
		tutoFinalPanel.SetActive(true);
	}
	public void TutoFinalEnd()
	{
		audio.clip = clickSound;
		audio.Play();
		timerActive = true;
		tutoFinalPanel.SetActive(false);
		hpBool = false;
		pHpBool = true;

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
	public void CheckShieldBroken()
	{
		if (ennemyShield.fillAmount <= 0)
		{
			ennemyShield.fillAmount = 0;
			shieldFB.SetActive(true);
			audio.clip = shieldBSound;
		audio.Play();
		}
		if (ennemyShield.fillAmount > 0)
		{
			audio.clip = shieldSound;
		audio.Play();
			shieldFB.SetActive(false);
		}
	}
}

