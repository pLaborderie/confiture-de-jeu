using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HowToPlayUI;
    public GameObject CreditsUI;

    public void Awake() {
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(false);
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
        HowToPlayUI.SetActive(true);
    }

    public void ShowCredits()
    {
        CreditsUI.SetActive(true);
    }

    public void CloseUI(GameObject _ui)
    {
        _ui.SetActive(false);
    }
}
