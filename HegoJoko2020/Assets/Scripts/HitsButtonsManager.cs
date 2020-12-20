using UnityEngine;
using UnityEngine.UI;
using System;

public class HitsButtonsManager : MonoBehaviour
{
  public GameObject buttonPrefab;
  public GameObject buttonLayout;
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
      button.transform.GetChild(0).GetComponent<Text>().text = Enum.GetName(typeof(AllHits), hit);
    }
  }
  private void HandleHit(AllHits hit)
  {
    gameObject.GetComponent<Hits>().SelectHit(hit);
  }
}
