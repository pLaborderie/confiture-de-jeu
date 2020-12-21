using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroMenu : MonoBehaviour
{
    public string[] citationsWin = new string[] {
                                            "Pourquoi être aussi corrompu ? Education de m*** ! Aucun honneur ! Aucune dignité ! Bon… vous avez gagné mais quand même !",
                                            "Et bah voilà c’est pas compliqué d’y arriver quand on met les moyens !",
                                            "Vous avez dépensé beaucoup mais cette combine vous rapportera gros !",
                                            "Le prochain match est dans quelques instants ! J’espère que votre petit truc marchera encore !",
                                            "Un double KO ? Non… Je suis certain que c’est une pure.. coïncidence !"
                                            };











    public string[] citationsLoose = new string[] {
                                            "Je crois que vous avez oublié quelques billets quand vous avez rempli cette mallette !", 
                                            "Hum… Un concurrent semble avoir mis plus d’argent sur le tapis ! Un de vos joueurs ne s’est pas couché !", 
                                            "Un petit malin a accepté les billets mais les victoires comptent plus que tout, vous vous êtes fait avoir !", 
                                            "Visiblement, même avec de l’argent, si le talent n’est pas là, il se fait mettre KO rapidement dis donc..",
                                            "Vous n’auriez pas dû échanger votre mallette avec celle de votre fille… Je ne suis pas certain que les bonbons soient une bonne motivation pour se coucher !",
                                            "J'ai horreur de perdre, ça me fait faire des otites !"};

    public Text ZoneTexte;
    public bool win;

    public void Awake()
    {
        if(win) {
            ShowRandomCitation(ZoneTexte, citationsWin.Length, citationsWin);
        } else {
            ShowRandomCitation(ZoneTexte, citationsLoose.Length, citationsLoose);
        }
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene("Ring");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowRandomCitation(Text _texte, int _size, string[] _citations) {
        System.Random rnd = new System.Random();
        int pos  = rnd.Next(_size);

        _texte.text = _citations[pos];
    }
}