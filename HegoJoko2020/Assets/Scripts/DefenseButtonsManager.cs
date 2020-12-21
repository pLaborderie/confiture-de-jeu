using System;
using Lean.Gui;
using UnityEngine;
using UnityEngine.UI;

public class DefenseButtonsManager : FightButtonsManager
{
    public void GenerateDefenseButtons(AllDefenseStances[] allDefenseStances)
    {
        ClearContent();
        foreach (AllDefenseStances stance in allDefenseStances)
        {
            // Create button from prefab and place it in vertical layout
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(buttonLayout.transform);
            // Add listener to deal damage when clicked
            button.GetComponent<LeanButton>().OnClick.AddListener(() => HandleDefend(stance));
            // Change text to hit name
            Text btnText = button.GetComponentInChildren<Text>();
            btnText.text = CommandName(stance);
            btnText.color = Color.white;
        }
    }
    private void HandleDefend(AllDefenseStances stance)
    {
        gameObject.GetComponent<DefenseStances>().SelectStance(stance);
        gameObject.GetComponent<Hits>().selectedHit = null;
        gameObject.GetComponent<FighterInfo>().n_nbTimeInARowToPerformDefensiveStance++;
    }

    private string CommandName(AllDefenseStances command)
    {
        switch(command)
        {
            case AllDefenseStances.UpBlock:
                return "Contre Coup Haut";
            case AllDefenseStances.DownBlock:
                return "Contre Coup Bas";
            case AllDefenseStances.UpDodge:
                return "Esquive Coup Haut";
            case AllDefenseStances.DownDodge:
                return "Esquive Coup Bas";
            default: return "";
        }
    }
}
