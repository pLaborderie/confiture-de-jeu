using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hits : MonoBehaviour
{
  public AllHits[] allHits;
  public AllHits? selectedHit;

  private Dictionary<AllHits, int> maxUse = new Dictionary<AllHits, int>();
  private Dictionary<AllHits, int> currentUse = new Dictionary<AllHits, int>();
  private Dictionary<AllHits, float> hitPower = new Dictionary<AllHits, float>();
  private Dictionary<AllHits, string> hitAnimation = new Dictionary<AllHits, string>();

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
          hitAnimation.Add(allHits[i], "upJab");
          break;
        case AllHits.DownJab:
          maxUse.Add(allHits[i], gameManager.HIT_DOWNJAB_MAXUSE);
          hitPower.Add(allHits[i], gameManager.HIT_DOWNJAB_HITPOWER);
          currentUse.Add(allHits[i], maxUse[allHits[i]]);
          hitAnimation.Add(allHits[i], "downJab");
          break;
        case AllHits.UpCross:
          maxUse.Add(allHits[i], gameManager.HIT_UPCROSS_MAXUSE);
          hitPower.Add(allHits[i], gameManager.HIT_UPCROSS_HITPOWER);
          currentUse.Add(allHits[i], maxUse[allHits[i]]);
          hitAnimation.Add(allHits[i], "upCross");
          break;
        case AllHits.DownCross:
          maxUse.Add(allHits[i], gameManager.HIT_DOWNCROSS_MAXUSE);
          hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
          currentUse.Add(allHits[i], maxUse[allHits[i]]);
          hitAnimation.Add(allHits[i], "downCross");
          break;
        case AllHits.Uppercut:
          maxUse.Add(allHits[i], gameManager.HIT_UPPERCUT_MAXUSE);
          hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
          currentUse.Add(allHits[i], maxUse[allHits[i]]);
          hitAnimation.Add(allHits[i], "uppercut");
          break;
      }
    }
    gameObject.GetComponent<HitsButtonsManager>().GenerateHitsButtons(allHits);
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
      DealHit((AllHits)selectedHit);
      selectedHit = null;
    }
  }

  public void DealHit(AllHits hit)
  {
    Debug.Log("Using hit");
    if (ArrayUtility.Contains<AllHits>(allHits, hit) && !gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idleKo"))
    {
      if (currentUse[hit] > 0)
      {
        gameManager.GetOpponentOf(gameObject).GetComponent<DefenseStances>().ReceiveHit(hit);
        gameObject.GetComponent<Animator>().Play(hitAnimation[hit]);
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
