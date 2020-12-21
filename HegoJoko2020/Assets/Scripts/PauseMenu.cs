using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameManager gameManager;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        if (gameManager.p_currentPhase == GameManager.Phase.SelectFirstMove)
        {
            gameManager.DisplayFighterButtons(gameManager.fighter1);
        }
        else if (gameManager.p_currentPhase == GameManager.Phase.SelectSecondMove)
        {
            gameManager.DisplayFighterButtons(gameManager.fighter2);
        }
    }

    public void Pause()
    {
        gameManager.HideFighterButtons(gameManager.fighter1);
        gameManager.HideFighterButtons(gameManager.fighter2);
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

