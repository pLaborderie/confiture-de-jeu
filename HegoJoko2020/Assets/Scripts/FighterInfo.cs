using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
<<<<<<< Updated upstream
  public float f_health;
  public Slider healthBar;
  public GameManager.Phase playablePhase;
  public GameObject hitFX;
=======
    public float f_health;
    public Slider healthBar;
    public GameManager gameManager;
    public GameManager.Phase playablePhase;
    public GameObject hitFX;
    public SoundManager soundManager;
    public AudioClip KnockOut;
>>>>>>> Stashed changes

    private Dictionary<AllKoStances, string> koStanceAnimation = new Dictionary<AllKoStances, string>();

    void Start()
    {
        koStanceAnimation.Add(AllKoStances.UpKo, "upKo");
        koStanceAnimation.Add(AllKoStances.DownKo, "downKo");
    }

    void Update()
    {
        healthBar.value = f_health;
    }

    public void TakeDamage(float damageDealt, AllHits hit)
    {
        f_health -= (damageDealt > f_health ? f_health : damageDealt);
        hitFX.GetComponent<Animator>().Play("hitFX");

        if (f_health == 0)
        {
            switch (hit)
            {
                case AllHits.UpJab:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.UpKo]);
                    soundManager.PlaySingle(KnockOut);
                    break;
                case AllHits.DownJab:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.DownKo]);
                    soundManager.PlaySingle(KnockOut);
                    break;
                case AllHits.UpCross:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.UpKo]);
                    soundManager.PlaySingle(KnockOut);
                    break;
                case AllHits.DownCross:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.DownKo]);
                    soundManager.PlaySingle(KnockOut);
                    break;
                case AllHits.Uppercut:
                    gameObject.GetComponent<Animator>().Play(koStanceAnimation[AllKoStances.UpKo]);
                    soundManager.PlaySingle(KnockOut);
                    break;
            }
        }
    }
}
