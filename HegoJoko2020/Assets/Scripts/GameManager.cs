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

public class GameManager : MonoBehaviour
{
    public int HIT_UPJAB_MAXUSE = 3;
    public int HIT_DOWNJAB_MAXUSE = 3;
    public int HIT_UPCROSS_MAXUSE = 3;
    public int HIT_DOWNCROSS_MAXUSE = 3;
    public int HIT_UPPERCUT_MAXUSE = 3;

    public float HIT_UPJAB_HITPOWER = 10;
    public float HIT_DOWNJAB_HITPOWER = 10;
    public float HIT_UPCROSS_HITPOWER = 10;
    public float HIT_DOWNCROSS_HITPOWER = 10;
    public float HIT_UPPERCUT_HITPOWER = 30;

    public float DEFENSESTANCE_UPBLOCK_DAMAGEREDUCTION = 5;
    public float DEFENSESTANCE_DOWNBLOCK_DAMAGEREDUCTION = 5;
    public float DEFENSESTANCE_UPDODGE_DAMAGEREDUCTION = 0;
    public float DEFENSESTANCE_DOWNDODGE_DAMAGEREDUCTION = 0;

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
    private Phase p_currentPhase;

    public GameObject fighter1;
    public GameObject fighter2;

    private void Awake()
    {
        if (_instance == null)
        {
            CreateInstance();
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
    }
    private static void ChangePhase(Phase _newPhase)
    {
        _instance.p_currentPhase = _newPhase;
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
}