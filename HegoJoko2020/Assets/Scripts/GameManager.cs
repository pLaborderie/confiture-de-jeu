using UnityEngine;

public class GameManager : MonoBehaviour
{
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
  private void ChangePhase(Phase _newPhase)
  {
    _instance.p_currentPhase = _newPhase;
  }

  private GameManager GetInstance()
  {
    return _instance;
  }
}