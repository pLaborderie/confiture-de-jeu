using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public int Time;

    public void Update()
    {
        if(fighter1 != null && fighter2 != null) {
            if(fighter1.GetComponent<FighterInfo>().b_hasTakenHit && fighter2.GetComponent<FighterInfo>().b_hasTakenHit) {
                fighter1.GetComponent<FighterInfo>().b_hasTakenHit = false;
                fighter2.GetComponent<FighterInfo>().b_hasTakenHit = false;
                NextPhase();
            }
        }
    }

    private void Awake()
    {
        CreateInstance();
        KO.gameObject.SetActive(false);
        HideFighterLight(Light2);

        /*
                if (_instance == null)
                {
                    CreateInstance(); 
                }
                else
                {
                    Destroy(this);
                }
        */
    }

    private void CreateInstance()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
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
                StartCoroutine(loadEnd(Time, ""));
            }
            else if (fighter1Health <= 0)
            {
                p_currentPhase = Phase.FirstBoxerKnockedOut;
                StartCoroutine(loadEnd(Time, "J1"));
            }
            else if (fighter2Health <= 0)
            {
                p_currentPhase = Phase.SecondBoxerKnockedOut;
                StartCoroutine(loadEnd(Time, "J2"));
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
        UpdateFighterCommands(_fighter);
        _fighter.GetComponent<HitsButtonsManager>().SetVisibility(true);
        _fighter.GetComponent<DefenseButtonsManager>().SetVisibility(true);
    }

    private void HideFighterButtons(GameObject _fighter)
    {
        _fighter.GetComponent<HitsButtonsManager>().SetVisibility(false);
        _fighter.GetComponent<DefenseButtonsManager>().SetVisibility(false);
    }
    private void UpdateFighterCommands(GameObject _fighter)
    {
        CommandManager.Commands[] commands = _fighter.GetComponent<CommandManager>().GetCommandsForCurrentRound();
        CommandManager.Commands[] validHits = new CommandManager.Commands[] {
            CommandManager.Commands.UpJab,
            CommandManager.Commands.DownJab,
            CommandManager.Commands.UpCross,
            CommandManager.Commands.DownCross,
            CommandManager.Commands.Uppercut,
        };
        List<AllHits> hits = new List<AllHits>();
        List<AllDefenseStances> stances = new List<AllDefenseStances>();
        foreach (CommandManager.Commands command in commands)
        {
            if (Array.Exists(validHits, hit => hit == command))
            {
                hits.Add((AllHits)command);
            }
            else
            {
                stances.Add((AllDefenseStances)command - 5);
            }
        }
        _fighter.GetComponent<HitsButtonsManager>().GenerateHitsButtons(hits.ToArray());
        _fighter.GetComponent<DefenseButtonsManager>().GenerateDefenseButtons(stances.ToArray());
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
                Debug.Log("JOUEUR 1");
                Debug.Log(fighter1.GetComponent<CommandManager>().GetCommandsForCurrentRound()[0]);
                HideFighterLight(Light2);
                DisplayFighterLight(Light1);
                break;
            case Phase.SelectSecondMove:
                HideFighterButtons(fighter1);
                DisplayFighterButtons(fighter2);
                Debug.Log("JOUEUR 2");
                Debug.Log(fighter2.GetComponent<CommandManager>().GetCommandsForCurrentRound()[0]);
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

    IEnumerator loadEnd(float _time, string _name)
    {
        KO.gameObject.SetActive(true);
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene("Outro" + _name);
    }
}