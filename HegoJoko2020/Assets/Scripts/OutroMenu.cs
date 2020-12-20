﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroMenu : MonoBehaviour
{
    public void NewGame()
    {

        SceneManager.LoadScene("Ring");
    }

    public void Quit()
    {

        Application.Quit();
    }
}
