using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DefenseStances : MonoBehaviour
{
    public AllDefenseStances[] allDefenseStances;

    private Dictionary<AllDefenseStances, AllHits[]> hitBlocked;
    private Dictionary<AllDefenseStances, AllHits[]> hitDodged;
    private Dictionary<AllDefenseStances, float> damageReduction;

    private AllDefenseStances currentDefenseStance;
    private GameManager gameManager;

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
                    break;
                case AllDefenseStances.DownBlock:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross });
                    hitDodged.Add(allDefenseStances[i], new AllHits[0]);
                    damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_DOWNBLOCK_DAMAGEREDUCTION);
                    break;
                case AllDefenseStances.UpDodge:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
                    hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.UpJab, AllHits.UpCross, AllHits.Uppercut });
                    damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_UPDODGE_DAMAGEREDUCTION);
                    break;
                case AllDefenseStances.DownDodge:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
                    hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross});
                    damageReduction.Add(allDefenseStances[i], gameManager.DEFENSESTANCE_DOWNDODGE_DAMAGEREDUCTION);
                    break;
            }
        }

        gameManager = FindObjectOfType<GameManager>();
    }

    public void ReceiveHit(AllHits hitReceived)
    {
        if (ArrayUtility.Contains<AllHits>(hitDodged[currentDefenseStance], hitReceived))
        {
            // Il faut dodge le coup
        }
        else if (ArrayUtility.Contains<AllHits>(hitBlocked[currentDefenseStance], hitReceived))
        {
            //gameObject.TakeDamage(hitReceived.f_hitPower - currentDefenseStance.f_damageReduction);
        }
        else
        {
            //gameObject.TakeDamage(hitReceived.f_hitPower);
        }
    }
}
