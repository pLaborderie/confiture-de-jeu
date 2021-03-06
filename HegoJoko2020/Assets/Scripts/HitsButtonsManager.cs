﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Lean.Gui;

public class HitsButtonsManager : FightButtonsManager
{
    // public GameObject fighter;
    public void GenerateHitsButtons(AllHits[] allHits)
    {
        ClearContent();
        foreach (AllHits hit in allHits)
        {
            // Create button from prefab and place it in vertical layout
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(buttonLayout.transform);
            // Add listener to deal damage when clicked
            button.GetComponent<LeanButton>().OnClick.AddListener(() => HandleHit(hit));
            // Change text to hit name
            // Coroutine needed to wait for fighter stats to be randomized
            StartCoroutine(SetButtonText(button, hit));
        }
    }
    private IEnumerator SetButtonText(GameObject _button, AllHits _hit)
    {
        float hitDmg = 0f;
        while (hitDmg == 0)
        {
            hitDmg = gameObject.GetComponent<Hits>().GetHitPower(_hit);
            Text btnText = _button.GetComponentInChildren<Text>();
            btnText.text = CommandName(_hit) + " - " + hitDmg + " PV";
            btnText.color = Color.white;
            yield return new WaitForFixedUpdate();
        }
    }
    private void HandleHit(AllHits hit)
    {
        gameObject.GetComponent<Hits>().SelectHit(hit);
    }

    private string CommandName(AllHits command)
    {
        switch (command)
        {
            case AllHits.UpJab:
                return "Jab Haut";
            case AllHits.DownJab:
                return "Jab Bas";
            case AllHits.UpCross:
                return "Direct Haut";
            case AllHits.DownCross:
                return "Direct Bas";
            case AllHits.Uppercut:
                return "Uppercut";
            default: return "";
        }
    }
}
