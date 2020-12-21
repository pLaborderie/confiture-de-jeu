using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroMenu : MonoBehaviour
{
    public string[] citations = new string[] {
                                            "En boxe, vous créez une stratégie pour battre chaque nouvel adversaire, c’est comme les échecs.", 
                                            "Pour gagner, il faut accepter de perdre.", 
                                            "Il n'y a qu'une réponse à la défaite, et c'est la victoire", 
                                            "La défaite est un pont vers la victoire."};
    public Text ZoneTexte;

    public void Awake()
    {
        ShowRandomCitation(ZoneTexte);
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene("Ring");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowRandomCitation(Text _texte) {
        System.Random rnd = new System.Random();
        int pos  = rnd.Next(5);

        _texte.text = citations[pos];
    }
}