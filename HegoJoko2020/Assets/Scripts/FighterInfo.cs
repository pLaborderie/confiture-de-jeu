using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
  public float f_health;
  public Slider healthBar;
  public GameManager.Phase playablePhase;
  public GameObject hitFX;

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
                break;
            case AllHits.DownJab:
                animation = "downHurt";
                break;
            case AllHits.UpCross:
                animation = "upHurt";
                break;
            case AllHits.DownCross:
               animation = "downHurt";
               break;
            case AllHits.Uppercut:
               animation = "upHurt";
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
