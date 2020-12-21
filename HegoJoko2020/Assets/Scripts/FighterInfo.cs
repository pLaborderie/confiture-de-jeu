using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FighterInfo : MonoBehaviour
{
    private const float MAX_HEALTH = 150f;
    private const float MIN_HEALTH = 80f;
    private const float MAX_STRENGTH = 1.5f;
    private const float MIN_STRENGTH = 0.8f;
    public float f_health;
    public float f_startHealth;
    public float f_strength;
    public Slider healthBar;
    public Text healthIndicator;
    public GameManager gameManager;
    public GameManager.Phase playablePhase;
    public GameObject hitFX;
    public SoundManager soundManager;
    public AudioClip KnockOut;
    public AudioClip ReceiveUpJab;
    public AudioClip ReceiveDownJab;
    public AudioClip ReceiveUpCross;
    public AudioClip ReceiveDownCross;
    public AudioClip ReceiveUppercut;
    public bool b_hasTakenHit;
    public RuntimeAnimatorController[] fightersAnimations = new RuntimeAnimatorController[6];

    private Dictionary<AllKoStances, string> koStanceAnimation = new Dictionary<AllKoStances, string>();

    void Start()
    {
        if(PlayerPrefs.GetInt("NbFighter1") > 0 && PlayerPrefs.GetInt("NbFighter1") <= 6)
        {
            if (gameManager.GetOpponentOf(gameManager.GetOpponentOf(gameObject)) == gameManager.fighter1)
            {
                GetComponent<Animator>().runtimeAnimatorController = fightersAnimations[PlayerPrefs.GetInt("NbFighter1")-1];
            }
            else
            {
                GetComponent<Animator>().runtimeAnimatorController = fightersAnimations[PlayerPrefs.GetInt("NbFighter2")-1];
            }
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = fightersAnimations[1];
        }
        
        b_hasTakenHit = false;
        koStanceAnimation.Add(AllKoStances.UpKo, "upKo");
        koStanceAnimation.Add(AllKoStances.DownKo, "downKo");
        CreateRandomStats();
    }

    void Update()
    {
        healthBar.value = f_health;
        healthIndicator.text = GetHealthText();
    }

    public void TakeDamage(float damageDealt, AllHits hit)
    {
        f_health -= (damageDealt > f_health ? f_health : damageDealt);
        hitFX.GetComponent<Animator>().Play("hitFX");

        if (f_health == 0)
        {
            switch (hit)
            {
                case AllHits.UpJab:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.UpKo]);
                    break;
                case AllHits.DownJab:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.DownKo]);
                    break;
                case AllHits.UpCross:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.UpKo]);
                    break;
                case AllHits.DownCross:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.DownKo]);
                    break;
                case AllHits.Uppercut:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.UpKo]);
                    break;
            }

            soundManager.PlaySingle(KnockOut);
        }
        if (gameManager.p_currentPhase == GameManager.Phase.ApplyMoves)
        {
            b_hasTakenHit = true;
        }
    }

    private void CreateRandomStats()
    {
        float newHealth = UnityEngine.Random.Range(MIN_HEALTH, MAX_HEALTH);
        SetDefaultHealth((float)Math.Round(newHealth));
        float newStrength = UnityEngine.Random.Range(MIN_STRENGTH, MAX_STRENGTH);
        f_strength = (float)Math.Round(newStrength, 2);
    }

    private void SetDefaultHealth(float _health)
    {
        f_health = _health;
        f_startHealth = _health;
        healthBar.maxValue = _health;
        healthIndicator.text = GetHealthText();
    }

    private string GetHealthText()
    {
        return f_health + "/" + f_startHealth + "PV";
    }
}
