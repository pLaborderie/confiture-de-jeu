using System;
using UnityEngine;
using UnityEngine.UI;

public class DefenseButtonsManager : MonoBehaviour
{
  public GameObject buttonPrefab;
  public GameObject buttonLayout;
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
      button.transform.GetChild(0).GetComponent<Text>().text = Enum.GetName(typeof(AllDefenseStances), stance);
    }
  }
  private void HandleDefend(AllDefenseStances stance)
  {
    gameObject.GetComponent<DefenseStances>().SetStance(stance);
  }
}
