using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hits : MonoBehaviour
{
    public AllHits[] allHits;

    private Dictionary<AllHits, int> maxUse;
    private Dictionary<AllHits, int> currentUse;
    private Dictionary<AllHits, float> hitPower;

    private GameManager gameManager;

    void Start()
    {
        for(int i = 0; i < allHits.Length; i++)
        {
            switch (allHits[i])
            {
                case AllHits.UpJab:
                    maxUse.Add(allHits[i], gameManager.HIT_UPJAB_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_UPJAB_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    break;
                case AllHits.DownJab:
                    maxUse.Add(allHits[i], gameManager.HIT_DOWNJAB_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNJAB_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    break;
                case AllHits.UpCross:
                    maxUse.Add(allHits[i], gameManager.HIT_UPCROSS_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_UPCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    break;
                case AllHits.DownCross:
                    maxUse.Add(allHits[i], gameManager.HIT_DOWNCROSS_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    break;
                case AllHits.Uppercut:
                    maxUse.Add(allHits[i], gameManager.HIT_UPPERCUT_MAXUSE);
                    hitPower.Add(allHits[i], gameManager.HIT_DOWNCROSS_HITPOWER);
                    currentUse.Add(allHits[i], maxUse[allHits[i]]);
                    break;
            }
        }

        gameManager = FindObjectOfType<GameManager>();
    }

    public void DealHit(AllHits hit)
    {
        if (ArrayUtility.Contains<AllHits>(allHits, hit))
        {
            if (currentUse[hit] > 0)
            {
                gameManager.GetOpponentOf(gameObject).GetComponent<DefenseStances>().ReceiveHit(hit);
            }
        }
        else
        {
            Debug.Log("This fighter doesn't know how to perform this hit...");
        }
    }
}