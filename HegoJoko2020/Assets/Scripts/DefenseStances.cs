using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DefenseStances : MonoBehaviour
{
  public AllDefenseStances[] allDefenseStances;

  private Dictionary<AllDefenseStances, AllHits[]> hitBlocked = new Dictionary<AllDefenseStances, AllHits[]>();
  private Dictionary<AllDefenseStances, AllHits[]> hitDodged = new Dictionary<AllDefenseStances, AllHits[]>();
  private Dictionary<AllDefenseStances, float> damageReduction = new Dictionary<AllDefenseStances, float>();
  private Dictionary<AllDefenseStances, string> defenseStanceAnimation = new Dictionary<AllDefenseStances, string>();

    private AllDefenseStances currentDefenseStance;
  public GameManager gameManager;

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

  public void SetStance(AllDefenseStances stance)
  {
    Debug.Log("New stance: " + Enum.GetName(typeof(AllDefenseStances), stance));
    currentDefenseStance = stance;
  }

  public void ReceiveHit(AllHits hitReceived)
  {
    if (ArrayUtility.Contains<AllHits>(hitDodged[currentDefenseStance], hitReceived) && gameObject.GetComponent<FighterInfo>().f_health > 0)
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
    else if (ArrayUtility.Contains<AllHits>(hitBlocked[currentDefenseStance], hitReceived) && gameObject.GetComponent<FighterInfo>().f_health > 0)
    {
            gameObject.GetComponent<FighterInfo>().TakeDamage(gameObject.GetComponent<Hits>().GetHitPower(hitReceived) - damageReduction[currentDefenseStance], hitReceived, defenseStanceAnimation[currentDefenseStance]);  
        }
    else
    {
        gameObject.GetComponent<FighterInfo>().TakeDamage(gameObject.GetComponent<Hits>().GetHitPower(hitReceived), hitReceived, "hurt");
     }
  }
}
