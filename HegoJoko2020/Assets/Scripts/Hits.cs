using System;
using System.Collections.Generic;
using UnityEngine;

public class Hits : MonoBehaviour
{
    public AllHits[] allHits;
    public AllHits? selectedHit;
    public GameManager gameManager;

    private Dictionary<AllHits, int> maxUse = new Dictionary<AllHits, int>();
    private Dictionary<AllHits, int> currentUse = new Dictionary<AllHits, int>();
    private Dictionary<AllHits, float> hitPower = new Dictionary<AllHits, float>();
    private Dictionary<AllHits, string> hitAnimation = new Dictionary<AllHits, string>();
    private Dictionary<AllHits, int> initialTimerAnimation = new Dictionary<AllHits, int>();
    private Dictionary<AllHits, int> currentTimerAnimation = new Dictionary<AllHits, int>();

    private bool b_isCurrentDealt;


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
                    maxUse.Add(allHits[i], gameManager.HIT_UPJAB_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_UPJAB_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    hitAnimation.Add(allHits[i], "upJab");
                    initialTimerAnimation.Add(allHits[i], (int)Mathf.Ceil(gameManager.HIT_UPJAB_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    currentTimerAnimation.Add(allHits[i], 0);
                    hit = ReceiveUpJab;
                    break;
                case AllHits.DownJab:
                    maxUse.Add(allHits[i], gameManager.HIT_DOWNJAB_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNJAB_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    hitAnimation.Add(allHits[i], "downJab");
                    initialTimerAnimation.Add(allHits[i], (int)Mathf.Ceil(gameManager.HIT_DOWNJAB_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    currentTimerAnimation.Add(allHits[i], 0);
                    hit = ReceiveDownJab;
                    break;
                case AllHits.UpCross:
                    maxUse.Add(allHits[i], gameManager.HIT_UPCROSS_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_UPCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    hitAnimation.Add(allHits[i], "upCross");
                    initialTimerAnimation.Add(allHits[i], (int)Mathf.Ceil(gameManager.HIT_UPCROSS_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    currentTimerAnimation.Add(allHits[i], 0);
                    hit = ReceiveUpCross;
                    break;
                case AllHits.DownCross:
                    maxUse.Add(allHits[i], gameManager.HIT_DOWNCROSS_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    hitAnimation.Add(allHits[i], "downCross");
                    initialTimerAnimation.Add(allHits[i], (int)Mathf.Ceil(gameManager.HIT_DOWNCROSS_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    currentTimerAnimation.Add(allHits[i], 0);
                    hit = ReceiveDownCross;
                    break;
                case AllHits.Uppercut:
                    maxUse.Add(allHits[i], gameManager.HIT_UPPERCUT_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_UPPERCUT_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    hitAnimation.Add(allHits[i], "uppercut");
                    initialTimerAnimation.Add(allHits[i], (int)Mathf.Ceil(gameManager.HIT_UPPERCUT_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    currentTimerAnimation.Add(allHits[i], 0);
                    hit = ReceiveUppercut;
                    break;
            }
        }
        selectedHit = null;
        b_isCurrentDealt = false;
        gameObject.GetComponent<HitsButtonsManager>().GenerateHitsButtons(allHits);
    }

    private void Update()
    {
        for (int i = 0; i < allHits.Length; i++)
        {
            if (currentTimerAnimation[allHits[i]] > 0)
            {
                currentTimerAnimation[allHits[i]]--;
            }
            else
            {
                if (b_isCurrentDealt)
                {
                    gameManager.GetOpponentOf(gameObject).GetComponent<DefenseStances>().ReceiveHit(selectedHit.GetValueOrDefault());
                    b_isCurrentDealt = false;
                    selectedHit = null;
                }
            }
        }
    }

    public void SelectHit(AllHits hit)
    {
        if (gameObject.GetComponent<FighterInfo>().playablePhase == gameManager.p_currentPhase)
        {
            selectedHit = hit;
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
            if (currentUse[selectedHit.GetValueOrDefault()] > 0)
            {
                soundManager.PlaySingle(hit);
                gameObject.GetComponent<Animator>().Play(hitAnimation[selectedHit.GetValueOrDefault()]);
                b_isCurrentDealt = true;
                currentTimerAnimation[selectedHit.GetValueOrDefault()] = initialTimerAnimation[selectedHit.GetValueOrDefault()];
            }
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