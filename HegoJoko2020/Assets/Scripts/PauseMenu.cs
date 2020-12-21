using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject fighter1;
    public GameObject fighter2;

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    private void HideFighterButtons(GameObject _fighter)
    {
        _fighter.GetComponent<HitsButtonsManager>().SetVisibility(false);
        _fighter.GetComponent<DefenseButtonsManager>().SetVisibility(false);
    }

    private void DisplayFighterButtons(GameObject _fighter)
    {
        _fighter.GetComponent<HitsButtonsManager>().SetVisibility(true);
        _fighter.GetComponent<DefenseButtonsManager>().SetVisibility(true);
    }

    public void Resume() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        DisplayFighterButtons(fighter1);
        DisplayFighterButtons(fighter2);
    }

    public void Pause() {
        HideFighterButtons(fighter1);
        HideFighterButtons(fighter2);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu()
    {

        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {

        Application.Quit();
    }
}

