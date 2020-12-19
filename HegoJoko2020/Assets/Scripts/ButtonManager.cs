using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public PauseMenu pauseFunction;

    public void NewGameBtn(string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitGameBtn() {
        Application.Quit();
    }

    public void LoadGameBtn() {
        //
    }
}
