using UnityEngine;
public enum AllHits
{
    UpJab,
    DownJab,
    UpCross,
    DownCross,
    Uppercut
}

public enum AllDefenseStances
{
    UpBlock,
    DownBlock,
    UpDodge,
    DownDodge
}

public enum AllHurtStances
{
    UpHurt,
    DownHurt
}

public enum AllKoStances
{
    UpKo,
    DownKo
}

public class GameManager : MonoBehaviour
{
    public int HIT_UPJAB_MAXUSE;
    public int HIT_DOWNJAB_MAXUSE;
    public int HIT_UPCROSS_MAXUSE;
    public int HIT_DOWNCROSS_MAXUSE;
    public int HIT_UPPERCUT_MAXUSE;

    public float HIT_UPJAB_HITPOWER;
    public float HIT_DOWNJAB_HITPOWER;
    public float HIT_UPCROSS_HITPOWER;
    public float HIT_DOWNCROSS_HITPOWER;
    public float HIT_UPPERCUT_HITPOWER;

    public int HIT_UPJAB_PERFORMFRAMEANIMATION;
    public int HIT_DOWNJAB_PERFORMFRAMEANIMATION;
    public int HIT_UPCROSS_PERFORMFRAMEANIMATION;
    public int HIT_DOWNCROSS_PERFORMFRAMEANIMATION;
    public int HIT_UPPERCUT_PERFORMFRAMEANIMATION;

    public float DEFENSESTANCE_UPBLOCK_DAMAGEREDUCTION;
    public float DEFENSESTANCE_DOWNBLOCK_DAMAGEREDUCTION;
    public float DEFENSESTANCE_UPDODGE_DAMAGEREDUCTION;
    public float DEFENSESTANCE_DOWNDODGE_DAMAGEREDUCTION;

    public int DEFENSESTANCE_UPBLOCK_PERFORMFRAMEANIMATION;
    public int DEFENSESTANCE_DOWNBLOCK_PERFORMFRAMEANIMATION;
    public int DEFENSESTANCE_UPDODGE_PERFORMFRAMEANIMATION;
    public int DEFENSESTANCE_DOWNDODGE_PERFORMFRAMEANIMATION;

    public int HURTSTANCE_UPHURT_PERFORMFRAMEANIMATION;
    public int HURTSTANCE_DOWNHURT_PERFORMFRAMEANIMATION;

    public enum Phase
    {
        SelectFirstMove,
        SelectSecondMove,
        ApplyMoves,
        FirstBoxerKnockedOut,
        SecondBoxerKnockedOut,
        DoubleKnockOut
    }

    private static GameManager _instance;
    public Phase p_currentPhase;

    public GameObject fighter1;
    public GameObject fighter2;

    public GameObject Light1;
    public GameObject Light2;
    public GameObject KO;


    private void Awake()
    {
        if (_instance == null)
        {
            CreateInstance();
            KO.gameObject.SetActive(false);
            HideFighterLight(Light2);            
        }
        else
        {
            Destroy(this);
        }
    }

    private void CreateInstance()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        PhaseTriggers();
    }

    private static GameManager GetInstance()
    {
        return _instance;
    }

    public GameObject GetOpponentOf(GameObject fighter)
    {
        if (fighter == fighter1)
        {
            return fighter2;
        }

        if (fighter == fighter2)
        {
            return fighter1;
        }

        return null;
    }

    public void NextPhase()
    {
        if (p_currentPhase < Phase.ApplyMoves)
        {
            p_currentPhase++;
        }
        else
        {
            float fighter1Health = fighter1.GetComponent<FighterInfo>().f_health;
            float fighter2Health = fighter2.GetComponent<FighterInfo>().f_health;
            if (fighter1Health <= 0 && fighter2Health <= 0)
            {
                p_currentPhase = Phase.DoubleKnockOut;
            }
            else if (fighter1Health <= 0)
            {
                p_currentPhase = Phase.FirstBoxerKnockedOut;
            }
            else if (fighter2Health <= 0)
            {
                p_currentPhase = Phase.SecondBoxerKnockedOut;
            }
            else
            {
                p_currentPhase = Phase.SelectFirstMove;
            }
        }

        PhaseTriggers();
    }
    private void DisplayFighterButtons(GameObject _fighter)
    {
        _fighter.GetComponent<HitsButtonsManager>().SetVisibility(true);
        _fighter.GetComponent<DefenseButtonsManager>().SetVisibility(true);
    }
    private void HideFighterButtons(GameObject _fighter)
    {
        _fighter.GetComponent<HitsButtonsManager>().SetVisibility(false);
        _fighter.GetComponent<DefenseButtonsManager>().SetVisibility(false);
    }
    private void DisplayFighterLight(GameObject _light)
    {
        _light.gameObject.SetActive(true);
    }
    private void HideFighterLight(GameObject _light)
    {
        _light.gameObject.SetActive(false);
    }
    private void PhaseTriggers()
    {
        Phase currentPhase = p_currentPhase;
        switch (currentPhase)
        {
            case Phase.SelectFirstMove:
                DisplayFighterButtons(fighter1);
                HideFighterLight(Light2);
                DisplayFighterLight(Light1);
                break;
            case Phase.SelectSecondMove:
                HideFighterButtons(fighter1);
                DisplayFighterButtons(fighter2);
                HideFighterLight(Light1);
                DisplayFighterLight(Light2);
                break;
            case Phase.ApplyMoves:
                HideFighterButtons(fighter2);
                fighter1.GetComponent<Hits>().ApplySelectedHit();
                fighter2.GetComponent<Hits>().ApplySelectedHit();
                break;
            default:
                break;
        }
    }
}