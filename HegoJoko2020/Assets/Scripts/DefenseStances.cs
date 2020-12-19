using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum AllDefenseStances
{
    UpBlock,
    DownBlock,
    UpDodge,
    DownDodge
}

public class DefenseStances : MonoBehaviour
{
    public AllDefenseStances[] allDefenseStances;

    private Dictionary<AllDefenseStances, AllHits[]> hitBlocked;
    private Dictionary<AllDefenseStances, AllHits[]> hitDodged;
    private Dictionary<AllDefenseStances, float> f_damageReduction;

    private AllDefenseStances currentDefenseStance;

    void Start()
    {
        for (int i = 0; i < allDefenseStances.Length; i++)
        {
            switch (allDefenseStances[i])
            {
                case AllDefenseStances.UpBlock:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[] { AllHits.UpJab, AllHits.UpCross, AllHits.Uppercut });
                    hitDodged.Add(allDefenseStances[i], new AllHits[0]);
                    f_damageReduction.Add(allDefenseStances[i], 10);
                    break;
                case AllDefenseStances.DownBlock:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross });
                    hitDodged.Add(allDefenseStances[i], new AllHits[0]);
                    f_damageReduction.Add(allDefenseStances[i], 10);
                    break;
                case AllDefenseStances.UpDodge:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
                    hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.UpJab, AllHits.UpCross, AllHits.Uppercut });
                    f_damageReduction.Add(allDefenseStances[i], 0);
                    break;
                case AllDefenseStances.DownDodge:
                    hitBlocked.Add(allDefenseStances[i], new AllHits[0]);
                    hitDodged.Add(allDefenseStances[i], new AllHits[] { AllHits.DownJab, AllHits.DownCross});
                    f_damageReduction.Add(allDefenseStances[i], 0);
                    break;
            }
        }
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
