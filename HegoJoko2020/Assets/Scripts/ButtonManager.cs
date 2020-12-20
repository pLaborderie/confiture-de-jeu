using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void NewGameBtn(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void ExitGameBtn() {
        Application.Quit();
    }

    public void LoadGameBtn() {
        //
    }
}
