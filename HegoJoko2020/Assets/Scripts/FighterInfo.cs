using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
    public float f_health;
	public Slider healthBar;

    void Start()
    {

    }

    void Update()
    {
        healthBar.value = f_health;
    }

    public void TakeDamage(float damageDealt)
	{
		f_health -= damageDealt;
		Debug.Log("Health = " + f_health.ToString());        
    }

    public void TakeDamage(float damageDealt, bool hitAnimation, AllHits hit)
    {
        if(!gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idleKo"))
        {
            f_health -= damageDealt;
            Debug.Log("Health = " + f_health.ToString());

            if (hitAnimation)
            {
                if (f_health > 0)
                {
                    switch (hit)
                    {
                        case AllHits.UpJab:
                            gameObject.GetComponent<Animator>().Play("upHurt");
                            break;
                        case AllHits.DownJab:
                            gameObject.GetComponent<Animator>().Play("downHurt");
                            break;
                        case AllHits.UpCross:
                            gameObject.GetComponent<Animator>().Play("upHurt");
                            break;
                        case AllHits.DownCross:
                            gameObject.GetComponent<Animator>().Play("downHurt");
                            break;
                        case AllHits.Uppercut:
                            gameObject.GetComponent<Animator>().Play("upHurt");
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
}
