using System;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DefenseStances : MonoBehaviour
{
    public AllDefenseStances[] allDefenseStances;
    public GameManager gameManager;

    private Dictionary<AllDefenseStances, AllHits[]> hitBlocked = new Dictionary<AllDefenseStances, AllHits[]>();
    private Dictionary<AllDefenseStances, AllHits[]> hitDodged = new Dictionary<AllDefenseStances, AllHits[]>();
    private Dictionary<AllDefenseStances, float> damageReduction = new Dictionary<AllDefenseStances, float>();
    private Dictionary<AllDefenseStances, string> defenseStanceAnimation = new Dictionary<AllDefenseStances, string>();
    private Dictionary<AllDefenseStances, int> defenseInitialTimerAnimation = new Dictionary<AllDefenseStances, int>();
    private Dictionary<AllDefenseStances, int> defenseCurrentTimerAnimation = new Dictionary<AllDefenseStances, int>();

    private Dictionary<AllHurtStances, string> hurtStanceAnimation = new Dictionary<AllHurtStances, string>();
    private Dictionary<AllHurtStances, int> hurtInitialTimerAnimation = new Dictionary<AllHurtStances, int>();
    private Dictionary<AllHurtStances, int> hurtCurrentTimerAnimation = new Dictionary<AllHurtStances, int>();

    private AllDefenseStances? currentDefenseStance;
    private AllHits? currentHitReceived;
    private bool b_isCurrentReceived;
    private bool b_isBlockingDamage;
  public SoundManager soundManager;
  public AudioClip ReceiveDodge;



    void Start()
  {
    for (int i = 0; i < allDefenseStances.Length; i++)
    {
      switch (allDefenseStances[i])
      {
        case AllDefenseStances.UpBlock:
          hitBlocked.Add(allDefenseStances[i], new AllHits[] { AllHits.UpJab, AllHits.UpCross, AllHits.Uppercut });
          hitDodged.Add(allDefenseStances[i], new AllHits[0]);
          damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_UPBLOCK_DAMAGEREDUCTION);
          defenseStanceAnimation.Add(allDefenseStances[i], "upBlock");
          break;
        case AllDefenseStances.DownBlock:
          hitBlocked.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross });
          hitDodged.Add(allDefenseStances[i], new AllHits[0]);
          damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_DOWNBLOCK_DAMAGEREDUCTION);
          defenseStanceAnimation.Add(allDefenseStances[i], "downBlock");
          break;
        case AllDefenseStances.UpDodge:
          hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
          hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.UpJab, AllHits.UpCross, AllHits.Uppercut });
          damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_UPDODGE_DAMAGEREDUCTION);
          defenseStanceAnimation.Add(allDefenseStances[i], "upDodge");
          break;
        case AllDefenseStances.DownDodge:
          hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
          hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross });
          damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_DOWNDODGE_DAMAGEREDUCTION);
          defenseStanceAnimation.Add(allDefenseStances[i], "downDodge");
          break;
      }
    }
    gameObject.GetComponent<DefenseButtonsManager>().GenerateDefenseButtons(allDefenseStances);
  }

    void Start()
    {
        for (int i = 0; i < allDefenseStances.Length; i++)
        {
            switch (allDefenseStances[i])
            {
                case AllDefenseStances.UpBlock:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[] { AllHits.UpJab, AllHits.UpCross, AllHits.Uppercut });
                    hitDodged.Add(allDefenseStances[i], new AllHits[0]);
                    damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_UPBLOCK_DAMAGEREDUCTION);
                    defenseStanceAnimation.Add(allDefenseStances[i], "upBlock");
                    defenseInitialTimerAnimation.Add(allDefenseStances[i], (int)Mathf.Ceil(gameManager.DEFENSESTANCE_UPBLOCK_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    defenseCurrentTimerAnimation.Add(allDefenseStances[i], 0);
                    break;
                case AllDefenseStances.DownBlock:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross });
                    hitDodged.Add(allDefenseStances[i], new AllHits[0]);
                    damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_DOWNBLOCK_DAMAGEREDUCTION);
                    defenseStanceAnimation.Add(allDefenseStances[i], "downBlock");
                    defenseInitialTimerAnimation.Add(allDefenseStances[i], (int)Mathf.Ceil(gameManager.DEFENSESTANCE_DOWNBLOCK_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    defenseCurrentTimerAnimation.Add(allDefenseStances[i], 0);
                    break;
                case AllDefenseStances.UpDodge:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
                    hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.UpJab, AllHits.UpCross, AllHits.Uppercut });
                    damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_UPDODGE_DAMAGEREDUCTION);
                    defenseStanceAnimation.Add(allDefenseStances[i], "upDodge");
                    defenseInitialTimerAnimation.Add(allDefenseStances[i], (int)Mathf.Ceil(gameManager.DEFENSESTANCE_UPDODGE_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    defenseCurrentTimerAnimation.Add(allDefenseStances[i], 0);
                    break;
                case AllDefenseStances.DownDodge:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
                    hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross });
                    damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_DOWNDODGE_DAMAGEREDUCTION);
                    defenseStanceAnimation.Add(allDefenseStances[i], "downDodge");
                    defenseInitialTimerAnimation.Add(allDefenseStances[i], (int)Mathf.Ceil(gameManager.DEFENSESTANCE_DOWNDODGE_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
                    defenseCurrentTimerAnimation.Add(allDefenseStances[i], 0);
                    break;
            }
        }

        hurtStanceAnimation.Add(AllHurtStances.UpHurt, "upHurt");
        hurtInitialTimerAnimation.Add(AllHurtStances.UpHurt, (int)Mathf.Ceil(gameManager.HURTSTANCE_UPHURT_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
        hurtCurrentTimerAnimation.Add(AllHurtStances.UpHurt, 0);

        hurtStanceAnimation.Add(AllHurtStances.DownHurt, "downHurt");
        hurtInitialTimerAnimation.Add(AllHurtStances.DownHurt, (int)Mathf.Ceil(gameManager.HURTSTANCE_DOWNHURT_PERFORMFRAMEANIMATION / gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed));
        hurtCurrentTimerAnimation.Add(AllHurtStances.DownHurt, 0);

        b_isCurrentReceived = false;
        gameObject.GetComponent<DefenseButtonsManager>().GenerateDefenseButtons(allDefenseStances);
    }

    private void Update()
    {
        for (int i = 0; i < allDefenseStances.Length; i++)
        {
            if (defenseCurrentTimerAnimation[allDefenseStances[i]] > 0)
            {
                defenseCurrentTimerAnimation[allDefenseStances[i]]--;
            }
            else if (b_isCurrentReceived)
            {
                if (b_isBlockingDamage)
                {
                    gameObject.GetComponent<FighterInfo>().TakeDamage(gameObject.GetComponent<Hits>().GetHitPower(currentHitReceived.GetValueOrDefault()) - damageReduction[currentDefenseStance.GetValueOrDefault()], currentHitReceived.GetValueOrDefault());
                    b_isBlockingDamage = false;
                }
                else
                {
                    gameObject.GetComponent<FighterInfo>().TakeDamage(gameObject.GetComponent<Hits>().GetHitPower(currentHitReceived.GetValueOrDefault()), currentHitReceived.GetValueOrDefault());
                }

                b_isCurrentReceived = false;
            }
        }

        if (hurtCurrentTimerAnimation[AllHurtStances.UpHurt] > 0)
        {
            hurtCurrentTimerAnimation[AllHurtStances.UpHurt]--;
        }
        else if (b_isCurrentReceived)
        {
            gameObject.GetComponent<FighterInfo>().TakeDamage(gameObject.GetComponent<Hits>().GetHitPower(currentHitReceived.GetValueOrDefault()), currentHitReceived.GetValueOrDefault());
            b_isCurrentReceived = false;
        }

    }

    public void SelectStance(AllDefenseStances stance)
    {
        if (gameObject.GetComponent<FighterInfo>().playablePhase == gameManager.p_currentPhase)
        {
            currentDefenseStance = stance;
            gameManager.NextPhase();
        }
        else
        {
            Debug.Log("Cannot change stance outside of fighter phase");
        }
    }

    public void ReceiveHit(AllHits? hitReceived)
    {
        if (hitReceived.HasValue)
        {
            if (currentDefenseStance.HasValue && ArrayUtility.Contains<AllHits>(hitDodged[currentDefenseStance.GetValueOrDefault()], hitReceived.GetValueOrDefault()))
            {
                switch (hitReceived)
                {
                    case AllHits.UpJab:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpDodge]);
                        break;
                    case AllHits.DownJab:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.DownDodge]);
                        break;
                    case AllHits.UpCross:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpDodge]);
                        break;
                    case AllHits.DownCross:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.DownDodge]);
                        break;
                    case AllHits.Uppercut:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpDodge]);
                        break;
                }
            }
            else if (currentDefenseStance.HasValue && ArrayUtility.Contains<AllHits>(hitBlocked[currentDefenseStance.GetValueOrDefault()], hitReceived.GetValueOrDefault()))
            {
                switch (hitReceived)
                {
                    case AllHits.UpJab:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpBlock]);
                        defenseCurrentTimerAnimation[AllDefenseStances.UpBlock] = defenseInitialTimerAnimation[AllDefenseStances.UpBlock];
                        break;
                    case AllHits.DownJab:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.DownBlock]);
                        defenseCurrentTimerAnimation[AllDefenseStances.DownBlock] = defenseInitialTimerAnimation[AllDefenseStances.DownBlock];
                        break;
                    case AllHits.UpCross:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpBlock]);
                        defenseCurrentTimerAnimation[AllDefenseStances.UpBlock] = defenseInitialTimerAnimation[AllDefenseStances.UpBlock];
                        break;
                    case AllHits.DownCross:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.DownBlock]);
                        defenseCurrentTimerAnimation[AllDefenseStances.DownBlock] = defenseInitialTimerAnimation[AllDefenseStances.DownBlock];
                        break;
                    case AllHits.Uppercut:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpBlock]);
                        defenseCurrentTimerAnimation[AllDefenseStances.UpBlock] = defenseInitialTimerAnimation[AllDefenseStances.UpBlock];
                        break;
                }

                b_isBlockingDamage = true;
                currentHitReceived = hitReceived.GetValueOrDefault();
                b_isCurrentReceived = true;
            }
            else
            {
                if (currentDefenseStance.HasValue)
                {
                    switch (hitReceived)
                    {
                        case AllHits.UpJab:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.UpHurt]);
                            hurtCurrentTimerAnimation[AllHurtStances.UpHurt] = hurtInitialTimerAnimation[AllHurtStances.UpHurt];
                            break;
                        case AllHits.DownJab:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.DownHurt]);
                            hurtCurrentTimerAnimation[AllHurtStances.DownHurt] = hurtInitialTimerAnimation[AllHurtStances.DownHurt];
                            break;
                        case AllHits.UpCross:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.UpHurt]);
                            hurtCurrentTimerAnimation[AllHurtStances.UpHurt] = hurtInitialTimerAnimation[AllHurtStances.UpHurt];
                            break;
                        case AllHits.DownCross:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.DownHurt]);
                            hurtCurrentTimerAnimation[AllHurtStances.DownHurt] = hurtInitialTimerAnimation[AllHurtStances.DownHurt];
                            break;
                        case AllHits.Uppercut:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.UpHurt]);
                            hurtCurrentTimerAnimation[AllHurtStances.UpHurt] = hurtInitialTimerAnimation[AllHurtStances.UpHurt];
                            break;
                    }
                }

                currentHitReceived = hitReceived.GetValueOrDefault();
                b_isCurrentReceived = true;
            }

            currentDefenseStance = null;
        }
        else if (currentDefenseStance.HasValue)
        {
            gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[currentDefenseStance.GetValueOrDefault()]);
        }
    }
}