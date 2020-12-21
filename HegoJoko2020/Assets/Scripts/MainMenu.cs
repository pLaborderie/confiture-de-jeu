using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HowToPlayPanel;
    public GameObject CreditsPanel;
    public GameObject OptionsPanel;

    public void Awake() {
        CloseAllPanels();
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            CloseAllPanels();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowHowToPlay()
    {
        HowToPlayPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        CreditsPanel.SetActive(true);
    }

    public void ShowOptions()
    {
        OptionsPanel.SetActive(true);
    }

    public void CloseAllPanels()
    {
        CloseUI(HowToPlayPanel);
        CloseUI(CreditsPanel);
        CloseUI(OptionsPanel);
    }

    public void CloseUI(GameObject _ui)
    {
        _ui.SetActive(false);
    }
}
