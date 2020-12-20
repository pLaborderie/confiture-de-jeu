using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public enum Commands
    {
        UpJab,
        DownJab,
        UpCross,
        DownCross,
        Uppercut,
        UpBlock,
        DownBlock,
        UpDodge,
        DownDodge
    }

    public enum Tags
    {
        zoneUp,
        zoneDown,
        powerWeak,
        powerMedium,
        powerStrong,
        typeOffense,
        typeDefense
    }

    private List<KeyValuePair<Tags, float>> CommandZone = new List<KeyValuePair<Tags, float>>();
    private List<KeyValuePair<Tags, float>> CommandPower = new List<KeyValuePair<Tags, float>>();
    private List<KeyValuePair<Tags, float>> CommandType = new List<KeyValuePair<Tags, float>>();

    private float[] UpJab = new float[3];
    private float[] DownJab = new float[3];
    private float[] UpCross = new float[3];
    private float[] DownCross = new float[3];
    private float[] Uppercut = new float[3];
    private float[] UpBlock = new float[3];
    private float[] DownBlock = new float[3];
    private float[] UpDodge = new float[3];
    private float[] DownDodge = new float[3];

    void Start()
    {
        // C'est dans ces variables qu'on stock les probas. Le [0] correspond à la valeur de zone,, le [1] à la valeur de force et le [2] à la valeur type.
        UpJab[0] = 5f / 9f; UpJab[1] = 4f / 9f; UpJab[2] = 5f / 9f;
        DownJab[0] = 4f / 9f; DownJab[1] = 2f / 9f; DownJab[2] = 5f / 9f;
        UpCross[0] = 5f / 9f; UpCross[1] = 2f / 9f; UpCross[2] = 5f / 9f;
        DownCross[0] = 4f / 9f; DownCross[1] = 4f / 9f; DownCross[2] = 5f / 9f;
        Uppercut[0] = 5f / 9f; Uppercut[1] = 3f / 9f; Uppercut[2] = 5f / 9f;
        UpBlock[0] = 5f / 9f; UpBlock[1] = 4f / 9f; UpBlock[2] = 4f / 9f;
        DownBlock[0] = 4f / 9f; DownBlock[1] = 4f / 9f; DownBlock[2] = 4f / 9f;
        UpDodge[0] = 5f / 9f; UpDodge[1] = 3f / 9f; UpDodge[2] = 4f / 9f;
        DownDodge[0] = 4f / 9f; DownDodge[1] = 3f / 9f; DownDodge[2] = 4f / 9f;

        // La command zone représente la zone d'action des coups : haut ou bas
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpJab[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownJab[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpCross[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownCross[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, Uppercut[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpBlock[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownBlock[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpDodge[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownDodge[0]));

        // La commande power représente la puissance d'action des coups : faible, moyen ou fort
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, UpJab[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, DownJab[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerMedium, UpCross[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerMedium, DownCross[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerStrong, Uppercut[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, UpBlock[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, DownBlock[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerStrong, UpDodge[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerStrong, DownDodge[1]));

        // La commande type représente le type d'action des coups : offensif ou défensif
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, UpJab[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, DownJab[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, UpCross[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, DownCross[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, Uppercut[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, UpBlock[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, DownBlock[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, UpDodge[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, DownDodge[2]));
    }

    public void RefreshProbabilities(AllHits hit)
    {
        ComputeProbabilities((Commands)Enum.Parse(typeof(Commands), hit.ToString()));
    }

    public void RefreshProbabilities(AllDefenseStances defenseStances)
    {
        ComputeProbabilities((Commands)Enum.Parse(typeof(Commands), defenseStances.ToString()));
    }

    private void ComputeProbabilities(Commands lastCommandUsed)
    {
        // C'est ici qu'on actualise les valeurs de proba en fonction du dernier hit ou stance utilisé par le joueur.
        switch (lastCommandUsed.ToString())
        {
            case "UpJab":
                // Ordre du tableau de tag : {valeur à baisser, valeur à augmenter légèrement, valeur à augmenter)
                RefreshValues(new Tags[] { Tags.zoneUp, Tags.zoneDown }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerWeak, Tags.powerMedium, Tags.powerStrong }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeOffense, Tags.typeDefense }, CommandType);
                break;
            case "DownJab":
                RefreshValues(new Tags[] { Tags.zoneDown, Tags.zoneUp }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerWeak, Tags.powerMedium, Tags.powerStrong }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeOffense, Tags.typeDefense }, CommandType);
                break;
            case "UpCross":
                RefreshValues(new Tags[] { Tags.zoneUp, Tags.zoneDown }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerMedium, Tags.powerWeak, Tags.powerStrong }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeOffense, Tags.typeDefense }, CommandType);
                break;
            case "DownCross":
                RefreshValues(new Tags[] { Tags.zoneDown, Tags.zoneUp }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerMedium, Tags.powerWeak, Tags.powerStrong }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeOffense, Tags.typeDefense }, CommandType);
                break;
            case "Uppercut":
                RefreshValues(new Tags[] { Tags.zoneUp, Tags.zoneDown }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerStrong, Tags.powerMedium, Tags.powerWeak }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeOffense, Tags.typeDefense }, CommandType);
                break;
            case "UpBlock":
                RefreshValues(new Tags[] { Tags.zoneUp, Tags.zoneDown }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerWeak, Tags.powerMedium, Tags.powerStrong }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeDefense, Tags.typeOffense }, CommandType);
                break;
            case "DownBlock":
                RefreshValues(new Tags[] { Tags.zoneDown, Tags.zoneUp }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerWeak, Tags.powerMedium, Tags.powerStrong }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeDefense, Tags.typeOffense }, CommandType);
                break;
            case "UpDodge":
                RefreshValues(new Tags[] { Tags.zoneUp, Tags.zoneDown }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerStrong, Tags.powerMedium, Tags.powerWeak }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeDefense, Tags.typeOffense }, CommandType);
                break;
            case "DownDodge":
                RefreshValues(new Tags[] { Tags.zoneDown, Tags.zoneUp }, CommandZone);
                RefreshValues(new Tags[] { Tags.powerStrong, Tags.powerMedium, Tags.powerWeak }, CommandPower);
                RefreshValues(new Tags[] { Tags.typeDefense, Tags.typeOffense }, CommandType);
                break;
        }
    }

    private void ReaffectVariables()
    {
        // Il faut réaffecter les variables UpJab[0], UpJab[1], ..., DownDodge[1], DownDodge[2] pour qu'ils aient les valeurs présentes dans les listes Command Zone, Power et Type
        // Sinon, elles ne sont pas actualisées pour pouvoir faire les calculs qui permettront de générer les commandes d'action des joueurs (méthode GetCommandsForCurrentRound) 
    }

    private void RefreshValues(Tags[] tags, List<KeyValuePair<Tags, float>> list)
    {
        // On attribut les écarts de valeur ici. On crée une liste temporaire sinon Untiy se fâche très rouge si on modifie une liste dans un foreach.

        List<KeyValuePair<Tags, float>> temp = new List<KeyValuePair<Tags, float>>();

        foreach (KeyValuePair<Tags, float> entry in list)
        {
            switch (tags.Length)
            {
                case 1:
                    if (entry.Key == tags[0] && entry.Value > 1f / 9f)
                    {
                        temp.Add(new KeyValuePair<Tags, float>(tags[0], entry.Value - 1f / 9f));
                    }
                    break;
                case 2:
                    if (entry.Key == tags[0] && entry.Value > 1f / 9f)
                    {
                        temp.Add(new KeyValuePair<Tags, float>(tags[0], entry.Value - 1f / 9f));
                    }

                    if (entry.Key == tags[1] && entry.Value < 1f)
                    {
                        temp.Add(new KeyValuePair<Tags, float>(tags[0], entry.Value + 1f / 9f));
                    }
                    break;
                case 3:
                    if (entry.Key == tags[0] && entry.Value > 1f / 9f)
                    {
                        temp.Add(new KeyValuePair<Tags, float>(tags[0], entry.Value - 1f / 9f));
                    }

                    if (entry.Key == tags[1] && entry.Value < 1f)
                    {
                        temp.Add(new KeyValuePair<Tags, float>(tags[0], entry.Value + 1f / 18f));
                    }

                    if (entry.Key == tags[2] && entry.Value < 1f)
                    {
                        temp.Add(new KeyValuePair<Tags, float>(tags[0], entry.Value + 1f / 9f));
                    }
                    break;
            }
        }

        list = temp;
    }



    public Commands[] GetCommandsForCurrentRound()
    {
        // On appelle cette fonction pour recevoir les 3 commandes avec les plus gros taux de probabilité d'apparition. On gère pas le random bonus qui permet de ne pas être super
        // régulier sur les moves qui viennent.
        List<KeyValuePair<Commands, float>> sortedProbabilities = new List<KeyValuePair<Commands, float>>();
        
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.UpJab, UpJab[0] + UpJab[1] + UpJab[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.DownJab, DownJab[0] + DownJab[1] + DownJab[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.UpCross, UpCross[0] + UpCross[1] + UpCross[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.DownCross, DownCross[0] + DownCross[1] + DownCross[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.Uppercut, Uppercut[0] + Uppercut[1] + Uppercut[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.UpBlock, UpBlock[0] + UpBlock[1] + UpBlock[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.DownBlock, DownBlock[0] + DownBlock[1] + DownBlock[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.UpDodge, UpDodge[0] + UpDodge[1] + UpDodge[2]));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(Commands.DownDodge, DownDodge[0] + DownDodge[1] + DownDodge[2]));

        sortedProbabilities.Sort((x, y) => x.Value.CompareTo(y.Value));
        List<KeyValuePair<Commands, float>> finalList = sortedProbabilities.GetRange(0, 3);
        Commands[] returnedCommands = new Commands[3];

        for(int i = 0; i < finalList.Count; i++)
        {
            returnedCommands.SetValue(finalList[i].Key,i);
        }
        
        return returnedCommands;
    }
}
