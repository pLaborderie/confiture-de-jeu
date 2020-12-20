using System;
using UnityEngine;
using UnityEngine.UI;

public class DefenseButtonsManager : FightButtonsManager
{
    public void GenerateDefenseButtons(AllDefenseStances[] allDefenseStances)
    {
        foreach (AllDefenseStances stance in allDefenseStances)
        {
            // Create button from prefab and place it in vertical layout
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(buttonLayout.transform);
            // Add listener to deal damage when clicked
            button.GetComponent<Button>().onClick.AddListener(() => HandleDefend(stance));
            // Change text to hit name
            Text btnText = button.transform.GetChild(0).GetComponent<Text>();
            btnText.text = Enum.GetName(typeof(AllDefenseStances), stance);
            btnText.color = Color.white;
        }
    }
    private void HandleDefend(AllDefenseStances stance)
    {
        gameObject.GetComponent<DefenseStances>().SelectStance(stance);
        gameObject.GetComponent<Hits>().selectedHit = null;
    }
}
