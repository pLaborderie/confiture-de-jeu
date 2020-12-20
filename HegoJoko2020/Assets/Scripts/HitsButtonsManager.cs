using UnityEngine;
using UnityEngine.UI;
using System;

public class HitsButtonsManager : FightButtonsManager
{
    public void GenerateHitsButtons(AllHits[] allHits)
    {
        foreach (AllHits hit in allHits)
        {
            // Create button from prefab and place it in vertical layout
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(buttonLayout.transform);
            // Add listener to deal damage when clicked
            button.GetComponent<Button>().onClick.AddListener(() => HandleHit(hit));
            // Change text to hit name
            Text btnText = button.transform.GetChild(0).GetComponent<Text>();
            btnText.text = Enum.GetName(typeof(AllHits), hit);
            btnText.color = Color.white;
        }
    }
    private void HandleHit(AllHits hit)
    {
        gameObject.GetComponent<Hits>().SelectHit(hit);
    }
}
