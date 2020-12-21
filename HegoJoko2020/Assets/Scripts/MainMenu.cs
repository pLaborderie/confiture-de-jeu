using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject HowToPlayPanel;
    public GameObject CreditsPanel;
    public GameObject OptionsPanel;
    public GameObject FighterSelectionPanel;

    private bool b_isAFighterAlreadySelected;
    private bool b_canChooseFighter;

    public SoundManager soundManager;
    public AudioClip startFight;

    public void Awake()
    {
        CloseAllPanels();
    }

    public void Start()
    {
        b_canChooseFighter = true;
        b_isAFighterAlreadySelected = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllPanels();
        }
    }

    public void NewGame()
    {
        FighterSelectionPanel.SetActive(true);
    }

    public void StartGame()
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
        b_isAFighterAlreadySelected = false;
        CloseUI(HowToPlayPanel);
        CloseUI(CreditsPanel);
        CloseUI(OptionsPanel);
        CloseUI(FighterSelectionPanel);
    }

    public void CloseUI(GameObject _ui)
    {
        _ui.SetActive(false);
    }

    public void SelectFighter(int numFighter)
    {
        if(b_canChooseFighter)
        {
            PlayerPrefs.SetInt("NbFighter" + (b_isAFighterAlreadySelected ? 2.ToString() : 1.ToString()), numFighter);

            if (b_isAFighterAlreadySelected)
            {
                b_canChooseFighter = false;
                soundManager.PlaySingle(startFight);
                StartCoroutine(CallCoroutine(1.5f));
            }
            else
            {
                b_isAFighterAlreadySelected = true;
            }
        }
    }

    public void SetFighterSelected(GameObject text)
    {
        if(b_canChooseFighter)
        {
            text.GetComponent<Text>().color = new Color(255, 233, 0);
        }
    }

    IEnumerator CallCoroutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        CloseAllPanels();
        StartGame();
    }
}
