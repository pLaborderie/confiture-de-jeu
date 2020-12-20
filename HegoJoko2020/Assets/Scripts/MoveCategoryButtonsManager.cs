using UnityEngine;
using UnityEngine.UI;

public class MoveCategoryButtonsManager : FightButtonsManager
{
    public GameManager gameManager;
    public GameObject cancelButtonLayout;
    private void Start()
    {
        GenerateCategoriesButtons();
        GenerateCancelButton();
    }
    public void GenerateCategoriesButtons()
    {
        GameObject movesButton = (GameObject)Instantiate(buttonPrefab);
        movesButton.transform.SetParent(buttonLayout.transform);
        movesButton.GetComponent<Button>().onClick.AddListener(() => DisplayHits());
        movesButton.transform.GetChild(0).GetComponent<Text>().text = "Attaques";

        GameObject stancesButton = (GameObject)Instantiate(buttonPrefab);
        stancesButton.transform.SetParent(buttonLayout.transform);
        stancesButton.GetComponent<Button>().onClick.AddListener(DisplayDefense);
        stancesButton.transform.GetChild(0).GetComponent<Text>().text = "Défense";
    }

    public void GenerateCancelButton()
    {
        GameObject cancelButton = (GameObject)Instantiate(buttonPrefab);
        cancelButton.transform.SetParent(cancelButtonLayout.transform);
        cancelButton.GetComponent<Button>().onClick.AddListener(ClickCancel);
        cancelButton.transform.GetChild(0).GetComponent<Text>().text = "Retour";
    }
    public void DisplayHits()
    {
        SetVisibility(false);
        SetCancelVisibility(true);
        CurrentFighter().GetComponent<HitsButtonsManager>().SetVisibility(true);
    }
    public void DisplayDefense()
    {
        SetVisibility(false);
        SetCancelVisibility(true);
        CurrentFighter().GetComponent<DefenseButtonsManager>().SetVisibility(true);
    }
    public void ClickCancel()
    {
        SetVisibility(true);
        SetCancelVisibility(false);
    }
    public void SetCancelVisibility(bool b_visibility)
    {
        buttonLayout.SetActive(b_visibility);
    }
    private GameObject CurrentFighter()
    {
        if (gameManager.p_currentPhase == GameManager.Phase.SelectFirstMove)
        {
            return gameManager.fighter1;
        }
        return gameManager.fighter2;
    }
}
