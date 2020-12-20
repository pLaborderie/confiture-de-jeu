﻿using UnityEngine;
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

  public float DEFENSESTANCE_UPBLOCK_DAMAGEREDUCTION;
  public float DEFENSESTANCE_DOWNBLOCK_DAMAGEREDUCTION;
  public float DEFENSESTANCE_UPDODGE_DAMAGEREDUCTION;
  public float DEFENSESTANCE_DOWNDODGE_DAMAGEREDUCTION;

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
        ForceDoubleKO();
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
  private void ToggleFighterButtons(GameObject _fighter)
  {
    _fighter.GetComponent<HitsButtonsManager>().ToggleVisibility();
    _fighter.GetComponent<DefenseButtonsManager>().ToggleVisibility();
  }
  private void PhaseTriggers()
  {
    Phase currentPhase = p_currentPhase;
    switch (currentPhase)
    {
      case Phase.SelectFirstMove:
                fighter1.GetComponent<FighterInfo>().SetIsHitting(false);
                fighter2.GetComponent<FighterInfo>().SetIsHitting(false);
                ToggleFighterButtons(fighter1);
        break;
      case Phase.SelectSecondMove:
        ToggleFighterButtons(fighter1);
        ToggleFighterButtons(fighter2);
        break;
      case Phase.ApplyMoves:
        ToggleFighterButtons(fighter2);
        fighter1.GetComponent<Hits>().ApplySelectedHit();
        fighter2.GetComponent<Hits>().ApplySelectedHit();
        NextPhase();
        break;
      default:
        break;
    }
  }

  private void ForceDoubleKO()
    {
        fighter1.GetComponent<FighterInfo>().TakeDamage(0, AllHits.UpJab, "hurt");
        fighter2.GetComponent<FighterInfo>().TakeDamage(0, AllHits.UpJab, "hurt");
    }
}