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
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpJab[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownJab[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpCross[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownCross[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, Uppercut[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpBlock[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownBlock[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneUp, UpDodge[0]));
        CommandZone.Add(new KeyValuePair<Tags, float>(Tags.zoneDown, DownDodge[0]));

        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, UpJab[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, DownJab[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerMedium, UpCross[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerMedium, DownCross[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerStrong, Uppercut[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, UpBlock[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerWeak, DownBlock[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerStrong, UpDodge[1]));
        CommandPower.Add(new KeyValuePair<Tags, float>(Tags.powerStrong, DownDodge[1]));

        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, UpJab[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, DownJab[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, UpCross[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, DownCross[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeOffense, Uppercut[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, UpBlock[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, DownBlock[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, UpDodge[2]));
        CommandType.Add(new KeyValuePair<Tags, float>(Tags.typeDefense, DownDodge[2]));

        UpJab[0] = 5 / 9; UpJab[1] = 4 / 9; UpJab[2] = 5 / 9;
        DownJab[0] = 4 / 9; DownJab[1] = 2 / 9; DownJab[2] = 5 / 9;
        UpCross[0] = 5 / 9; UpCross[1] = 2 / 9; UpCross[2] = 5 / 9;
        DownCross[0] = 4 / 9; DownCross[1] = 4 / 9; DownCross[2] = 5 / 9;
        Uppercut[0] = 5 / 9; Uppercut[1] = 3 / 9; Uppercut[2] = 5 / 9;
        UpBlock[0] = 5 / 9; UpBlock[1] = 4 / 9; UpBlock[2] = 4 / 9;
        DownBlock[0] = 4 / 9; DownBlock[1] = 4 / 9; DownBlock[2] = 4 / 9;
        UpDodge[0] = 5 / 9; UpDodge[1] = 3 / 9; UpDodge[2] = 4 / 9;
        DownDodge[0] = 4 / 9; DownDodge[1] = 3 / 9; DownDodge[2] = 4 / 9;
    }

    public void RefreshProbabilities(AllHits hit)
    {
        Commands lastCommandUsed = (Commands)Enum.Parse(typeof(Commands), hit.ToString());

        switch (lastCommandUsed.ToString())
        {
            case "UpJab":
                // Ordre du tableau de tag : {valeur à baisser, valeur à augmenter légèrement, valeur à augmenter)
                RefreshValues(new Tags[] {Tags.zoneUp,Tags.zoneDown}, CommandZone);
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

    private void RefreshValues(Tags[] tags, List<KeyValuePair<Tags, float>> list)
    {
        foreach (KeyValuePair<Tags, float> entry in list)
        {
            switch(tags.Length)
            {
                case 1:
                    if (entry.Key == tags[0] && entry.Value > 1 / 9)
                    {
                        KeyValuePair<Tags, float> newKeyValuePair = new KeyValuePair<Tags, float>(tags[0], entry.Value - 1 / 9);
                        list[list.FindIndex(a => a.Key == tags[0] && a.Value == entry.Value)] = newKeyValuePair;
                    }
                    break;
                case 2:
                    if (entry.Key == tags[0] && entry.Value > 1 / 9)
                    {
                        KeyValuePair<Tags, float> newKeyValuePair = new KeyValuePair<Tags, float>(tags[0], entry.Value - 1 / 9);
                        list[list.FindIndex(a => a.Key == tags[0] && a.Value == entry.Value)] = newKeyValuePair;
                    }

                    if (entry.Key == tags[1] && entry.Value < 1)
                    {
                        KeyValuePair<Tags, float> newKeyValuePair = new KeyValuePair<Tags, float>(tags[1], entry.Value + 1 / 9);
                        list[list.FindIndex(a => a.Key == tags[1] && a.Value == entry.Value)] = newKeyValuePair;
                    }
                    break;
                case 3:
                    if (entry.Key == tags[0] && entry.Value > 1 / 9)
                    {
                        KeyValuePair<Tags, float> newKeyValuePair = new KeyValuePair<Tags, float>(tags[0], entry.Value - 1 / 9);
                        list[list.FindIndex(a => a.Key == tags[0] && a.Value == entry.Value)] = newKeyValuePair;
                    }

                    if (entry.Key == tags[1] && entry.Value < 1)
                    {
                        KeyValuePair<Tags, float> newKeyValuePair = new KeyValuePair<Tags, float>(tags[1], entry.Value + 1 / 18);
                        list[list.FindIndex(a => a.Key == tags[1] && a.Value == entry.Value)] = newKeyValuePair;
                    }

                    if (entry.Key == tags[2] && entry.Value < 1)
                    {
                        KeyValuePair<Tags, float> newKeyValuePair = new KeyValuePair<Tags, float>(tags[1], entry.Value + 1 / 9);
                        list[list.FindIndex(a => a.Key == tags[1] && a.Value == entry.Value)] = newKeyValuePair;
                    }
                    break;
            }
        }
    }
}
