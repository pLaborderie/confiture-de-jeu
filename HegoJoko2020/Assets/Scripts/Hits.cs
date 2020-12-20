using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hits : MonoBehaviour
{
  public AllHits[] allHits;

  private Dictionary<AllHits, int> maxUse = new Dictionary<AllHits, int>();
  private Dictionary<AllHits, int> currentUse = new Dictionary<AllHits, int>();
  private Dictionary<AllHits, float> hitPower = new Dictionary<AllHits, float>();
  private Dictionary<AllHits, string> animation = new Dictionary<AllHits, string>();

  public GameManager gameManager;
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
                    animation.Add(allHits[i], "upJab");
                    break;
                case AllHits.DownJab:
                    maxUse.Add(allHits[i], gameManager.HIT_DOWNJAB_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNJAB_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    animation.Add(allHits[i], "downJab");
                    break;
                case AllHits.UpCross:
                    maxUse.Add(allHits[i], gameManager.HIT_UPCROSS_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_UPCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    animation.Add(allHits[i], "upCross");
                    break;
                case AllHits.DownCross:
                    maxUse.Add(allHits[i], gameManager.HIT_DOWNCROSS_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    animation.Add(allHits[i], "downCross");
                    break;
                case AllHits.Uppercut:
                    maxUse.Add(allHits[i], gameManager.HIT_UPPERCUT_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    animation.Add(allHits[i], "uppercut");
                    break;
            }
        }
        gameObject.GetComponent<HitsButtonsManager>().GenerateHitsButtons(allHits);
  }

  public void DealHit(AllHits hit)
  {
        Debug.Log("Using hit");
        if (ArrayUtility.Contains<AllHits>(allHits, hit))
        {
            if (currentUse[hit] > 0)
            {
                gameManager.GetOpponentOf(gameObject).GetComponent<DefenseStances>().ReceiveHit(hit);
                gameObject.GetComponent<Animator>().Play(animation[hit]);
            }
        }
        else
        {
            Debug.Log("This fighter doesn't know how to perform this hit...");
        }
  }

  public float GetHitPower(AllHits hit)
  {
    return hitPower[hit];
  }

  public void SendDamage()
  {
    DealHit(AllHits.UpJab);
  }
}
