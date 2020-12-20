using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
  public float f_health;
  public Slider healthBar;
  public GameManager.Phase playablePhase;
  public GameObject hitFX;
  public SoundManager soundManager;
  public AudioClip ReceiveUpJab;
  public AudioClip ReceiveDownJab;
  public AudioClip ReceiveUpCross;
  public AudioClip ReceiveDownCross;
  public AudioClip ReceiveUppercut;

    private bool b_isHitting;

    void Start()
    {
        b_isHitting = false;
    }

    void Update()
  {
    healthBar.value = f_health;
  }

  public void TakeDamage(float damageDealt, AllHits hit, string animation)
  {
        f_health -= (damageDealt > f_health ? f_health : damageDealt);
        hitFX.GetComponent<Animator>().Play("hitFX");

    if (animation == "hurt" && !b_isHitting)
    {
       switch (hit)
       {
            case AllHits.UpJab:
                animation = "upHurt";
                soundManager.PlaySingle(ReceiveUpJab);
                break;
            case AllHits.DownJab:
                animation = "downHurt";
                soundManager.PlaySingle(ReceiveDownJab);
                break;
            case AllHits.UpCross:
                animation = "upHurt";
                soundManager.PlaySingle(ReceiveUpCross);
                break;
            case AllHits.DownCross:
               animation = "downHurt";
               soundManager.PlaySingle(ReceiveDownCross);
               break;
            case AllHits.Uppercut:
               animation = "upHurt";
               soundManager.PlaySingle(ReceiveUppercut);
               break;
       }

         gameObject.GetComponent<Animator>().Play(animation);
     }

      if (f_health == 0)
      {
                switch (hit)
                {
                    case AllHits.UpJab:
                        animation = "upKo";
                        break;
                    case AllHits.DownJab:
                        animation = "downKo";
                        break;
                    case AllHits.UpCross:
                        animation = "upKo";
                        break;
                    case AllHits.DownCross:
                        animation = "downKo";
                        break;
                    case AllHits.Uppercut:
                        animation = "upKo";
                        break;
                }

            gameObject.GetComponent<Animator>().Play(animation);
      }
  }

    public void SetIsHitting(bool b_value)
    {
        b_isHitting = b_value;
    }
}
