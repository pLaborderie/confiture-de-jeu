using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

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
            button.GetComponent<Button>().onClick.AddListener(() => HandleHit(hit));
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
            Text btnText = _button.transform.GetChild(0).GetComponent<Text>();
            btnText.text = Enum.GetName(typeof(AllHits), _hit) + " - " + hitDmg + " PV";
            btnText.color = Color.white;
            yield return new WaitForFixedUpdate();
        }
    }
    private void HandleHit(AllHits hit)
    {
        gameObject.GetComponent<Hits>().SelectHit(hit);
    }
}
