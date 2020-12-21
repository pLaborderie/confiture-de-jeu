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

    private Dictionary<AllHurtStances, string> hurtStanceAnimation = new Dictionary<AllHurtStances, string>();

    private AllDefenseStances? currentDefenseStance;

    public SoundManager soundManager;
    public AudioClip ReceiveDodge;
    public AudioClip ReceiveUpJab;
    public AudioClip ReceiveDownJab;
    public AudioClip ReceiveUpCross;
    public AudioClip ReceiveDownCross;
    public AudioClip ReceiveUppercut;

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
                    damageReduction.Add(allDefenseStances[i], 0);
                    defenseStanceAnimation.Add(allDefenseStances[i], "upDodge");
                    break;
                case AllDefenseStances.DownDodge:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
                    hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross });
                    damageReduction.Add(allDefenseStances[i], 0);
                    defenseStanceAnimation.Add(allDefenseStances[i], "downDodge");
                    break;
            }
        }

        hurtStanceAnimation.Add(AllHurtStances.UpHurt, "upHurt");
        hurtStanceAnimation.Add(AllHurtStances.DownHurt, "downHurt");

        gameObject.GetComponent<DefenseButtonsManager>().GenerateDefenseButtons(allDefenseStances);
    }

    public void SelectStance(AllDefenseStances stance)
    {
        if (gameObject.GetComponent<FighterInfo>().playablePhase == gameManager.p_currentPhase)
        {
            currentDefenseStance = stance;
            GetComponent<CommandManager>().RefreshProbabilities(stance);
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
            if (currentDefenseStance.HasValue && Array.Exists(hitDodged[currentDefenseStance.GetValueOrDefault()], hit => hit == hitReceived.GetValueOrDefault()))
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

                soundManager.PlaySingle(ReceiveDodge);
                gameManager.NextPhase();
            }
            else if (currentDefenseStance.HasValue && Array.Exists(hitBlocked[currentDefenseStance.GetValueOrDefault()], hit => hit == hitReceived.GetValueOrDefault()))
            {
                switch (hitReceived)
                {
                    case AllHits.UpJab:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpBlock]);
                        break;
                    case AllHits.DownJab:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.DownBlock]);
                        break;
                    case AllHits.UpCross:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpBlock]);
                        break;
                    case AllHits.DownCross:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.DownBlock]);
                        break;
                    case AllHits.Uppercut:
                        gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[AllDefenseStances.UpBlock]);
                        break;
                }

                gameObject.GetComponent<FighterInfo>().TakeDamage(gameManager.GetOpponentOf(gameObject).GetComponent<Hits>().GetHitPower(hitReceived.GetValueOrDefault()) - damageReduction[currentDefenseStance.GetValueOrDefault()], hitReceived.GetValueOrDefault());
            }
            else
            {
                if (currentDefenseStance.HasValue)
                {
                    switch (hitReceived)
                    {
                        case AllHits.UpJab:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.UpHurt]);
                            soundManager.PlaySingle(ReceiveUpJab);
                            break;
                        case AllHits.DownJab:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.DownHurt]);
                            soundManager.PlaySingle(ReceiveDownJab);
                            break;
                        case AllHits.UpCross:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.UpHurt]);
                            soundManager.PlaySingle(ReceiveUpCross);
                            break;
                        case AllHits.DownCross:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.DownHurt]);
                            soundManager.PlaySingle(ReceiveDownCross);
                            break;
                        case AllHits.Uppercut:
                            gameObject.GetComponent<Animator>().Play(hurtStanceAnimation[AllHurtStances.UpHurt]);
                            soundManager.PlaySingle(ReceiveUppercut);
                            break;
                    }
                }

                gameObject.GetComponent<FighterInfo>().TakeDamage(gameManager.GetOpponentOf(gameObject).GetComponent<Hits>().GetHitPower(hitReceived.GetValueOrDefault()), hitReceived.GetValueOrDefault());
            }
        }
        else if (currentDefenseStance.HasValue)
        {
            // Si aucun coup offensif n'est choisi, alors chaque fighter joue sa defense stance dans le vide
            gameObject.GetComponent<Animator>().Play(defenseStanceAnimation[currentDefenseStance.GetValueOrDefault()]);

            if (gameManager.p_currentPhase == GameManager.Phase.ApplyMoves)
            {
                gameManager.NextPhase();
            }
        }

        currentDefenseStance = null;
    }
}
