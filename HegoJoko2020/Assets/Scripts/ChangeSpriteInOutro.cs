using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteInOutro : MonoBehaviour
{
    public Sprite[] fightersSprites = new Sprite[6];
    public Sprite[] koSprites = new Sprite[6];

    public Sprite defaultSprite;
    public Sprite defaultKoSprite;

    public int n_numFighter;
    public bool b_isKo;

    private int n_nbSpriteToUse;

    void Start()
    {
        Debug.Log(n_numFighter);

        if (PlayerPrefs.GetInt("NbFighter" + n_numFighter.ToString()) > 0 && PlayerPrefs.GetInt("NbFighter" + n_numFighter.ToString()) <= 6)
        {
            n_nbSpriteToUse = PlayerPrefs.GetInt("NbFighter" + n_numFighter.ToString())-1;

            if (b_isKo)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = koSprites[n_nbSpriteToUse];
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = fightersSprites[n_nbSpriteToUse];
            }      
        }
        else
        {
            if (b_isKo)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultKoSprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
    }
}
