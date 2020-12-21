﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteInOutro : MonoBehaviour
{
    public Sprite[] fightersSprites = new Sprite[6];
    public Sprite[] koSprites = new Sprite[6];
    public Sprite defaultSprite;
    public Sprite defaultKoSprite;
    public int numFighter;
    public bool b_isKo;

    private int n_nbSpriteToUse;

    void Start()
    {
        if (PlayerPrefs.GetInt("NbFighter"+ numFighter.ToString()) > 0 && PlayerPrefs.GetInt("NbFighter" + numFighter.ToString()) <= 6)
        {
            n_nbSpriteToUse = PlayerPrefs.GetInt("NbFighter" + numFighter.ToString()) - 1;

            if (b_isKo)
            {
                GetComponent<SpriteRenderer>().sprite = koSprites[n_nbSpriteToUse];
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = fightersSprites[n_nbSpriteToUse];
            }      
        }
        else
        {
            if (b_isKo)
            {
                GetComponent<SpriteRenderer>().sprite = defaultKoSprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
    }
}