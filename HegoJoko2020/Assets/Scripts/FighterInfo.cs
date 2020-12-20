using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
    public float f_health;
	public Slider healthBar;
    public GameObject hitFX;

    void Update()
    {
        healthBar.value = f_health;
    }

    public void TakeDamage(float damageDealt, AllHits hit, string animation)
    {
        if(!gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idleKo"))
        {
            f_health -= damageDealt;

            if(animation == "hurt")
            {
                hitFX.GetComponent<Animator>().Play("hitFX");

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
            }

            if (f_health > 0)
            {
                switch (hit)
                {
                    case AllHits.UpJab:
                        gameObject.GetComponent<Animator>().Play(animation);
                        break;
                    case AllHits.DownJab:
                        gameObject.GetComponent<Animator>().Play(animation);
                        break;
                    case AllHits.UpCross:
                        gameObject.GetComponent<Animator>().Play(animation);
                        break;
                    case AllHits.DownCross:
                        gameObject.GetComponent<Animator>().Play(animation);
                        break;
                    case AllHits.Uppercut:
                        gameObject.GetComponent<Animator>().Play(animation);
                        break;
                }
            }
            else
            {
                switch (hit)
                {
                    case AllHits.UpJab:
                        gameObject.GetComponent<Animator>().Play("upKo");
                        break;
                    case AllHits.DownJab:
                        gameObject.GetComponent<Animator>().Play("downKo");
                        break;
                    case AllHits.UpCross:
                        gameObject.GetComponent<Animator>().Play("upKo");
                        break;
                    case AllHits.DownCross:
                        gameObject.GetComponent<Animator>().Play("downKo");
                        break;
                    case AllHits.Uppercut:
                        gameObject.GetComponent<Animator>().Play("upKo");
                        break;
                }
            }
        }
    }
}
