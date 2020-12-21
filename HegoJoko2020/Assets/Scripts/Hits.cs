using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hits : MonoBehaviour
{
    public AllHits[] allHits;
    public AllHits? selectedHit;
    public GameManager gameManager;

    private Dictionary<AllHits, float> hitPower = new Dictionary<AllHits, float>();
    private Dictionary<AllHits, string> hitAnimation = new Dictionary<AllHits, string>();

    public SoundManager soundManager;
    public AudioClip ReceiveUpJab;
    public AudioClip ReceiveDownJab;
    public AudioClip ReceiveUpCross;
    public AudioClip ReceiveDownCross;
    public AudioClip ReceiveUppercut;
    private AudioClip hit;

    void Start()
    {
        for (int i = 0; i < allHits.Length; i++)
        {
            switch (allHits[i])
            {
                case AllHits.UpJab:
                    hitPower.Add(allHits[i], gameManager.HIT_UPJAB_HITPOWER);
                    hitAnimation.Add(allHits[i], "upJab");
                    hit = ReceiveUpJab;
                    break;
                case AllHits.DownJab:
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNJAB_HITPOWER);
                    hitAnimation.Add(allHits[i], "downJab");
                    hit = ReceiveDownJab;
                    break;
                case AllHits.UpCross:
                    hitPower.Add(allHits[i], gameManager.HIT_UPCROSS_HITPOWER);
                    hitAnimation.Add(allHits[i], "upCross");
                    hit = ReceiveUpCross;
                    break;
                case AllHits.DownCross:
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
                    hitAnimation.Add(allHits[i], "downCross");
                    hit = ReceiveDownCross;
                    break;
                case AllHits.Uppercut:
                    hitPower.Add(allHits[i], gameManager.HIT_UPPERCUT_HITPOWER);
                    hitAnimation.Add(allHits[i], "uppercut");
                    hit = ReceiveUppercut;
                    break;
            }
        }
        selectedHit = null;
        gameObject.GetComponent<HitsButtonsManager>().GenerateHitsButtons(allHits);
    }

    public void SelectHit(AllHits hit)
    {
        if (gameObject.GetComponent<FighterInfo>().playablePhase == gameManager.p_currentPhase)
        {
            selectedHit = hit;
            gameObject.GetComponent<FighterInfo>().n_nbTimeInARowToPerformDefensiveStance = 0;
            gameManager.GetOpponentOf(gameObject).GetComponent<FighterInfo>().lastOpponenthit = hit;
            gameManager.NextPhase();
        }
        else
        {
            Debug.Log("Cannot select hit outside of fighter phase");
        }
    }

    public void ApplySelectedHit()
    {
        if (selectedHit != null)
        {
            DealHit();
        }
        else
        {
            // Si le fighter adverse n'utilise pas non plus de move, on remplit la condition
            if (gameObject.GetComponent<Hits>().gameManager.GetOpponentOf(gameObject).GetComponent<Hits>().selectedHit == null)
            {
                gameManager.GetOpponentOf(gameObject).GetComponent<DefenseStances>().ReceiveHit(null);
            }
        }
    }

    public void DealHit()
    {
        if (Array.Exists(allHits, hit => hit == selectedHit.GetValueOrDefault()))
        {
            soundManager.PlaySingle(hit);
            gameObject.GetComponent<Animator>().Play(hitAnimation[selectedHit.GetValueOrDefault()]);  
            GetComponent<CommandManager>().RefreshProbabilities(selectedHit.GetValueOrDefault());
            gameManager.GetOpponentOf(gameObject).GetComponent<DefenseStances>().ReceiveHit(selectedHit.GetValueOrDefault());
            selectedHit = null;
            
        }
        else
        {
            Debug.Log("This fighter doesn't know how to perform this hit...");
        }
    }

    public float GetHitPower(AllHits hit)
    {
        float strength = gameObject.GetComponent<FighterInfo>().f_strength;
        return (float)Math.Round(hitPower[hit] * strength);
    }
}