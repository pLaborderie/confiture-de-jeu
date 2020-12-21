using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private class Move
    {
        public Commands name;
        public Zone zone;
        public Power power;
        public Type type;
        public Move(Commands _name, Zone _zone, Power _power, Type _type)
        {
            name = _name;
            zone = _zone;
            power = _power;
            type = _type;
        }
    }
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

    public enum Zone
    {
        Up,
        Down
    }
    public enum Power
    {
        Weak,
        Medium,
        Strong
    }
    public enum Type
    {
        Offense,
        Defense
    }

    private Move[] moves = new Move[] {
        new Move(Commands.UpJab, Zone.Up, Power.Weak, Type.Offense),
        new Move(Commands.DownJab, Zone.Down, Power.Weak, Type.Offense),
        new Move(Commands.UpCross, Zone.Up, Power.Medium, Type.Offense),
        new Move(Commands.DownCross, Zone.Down, Power.Medium, Type.Offense),
        new Move(Commands.Uppercut, Zone.Up, Power.Strong, Type.Offense),
        new Move(Commands.UpBlock, Zone.Up, Power.Weak, Type.Defense),
        new Move(Commands.DownBlock, Zone.Down, Power.Weak, Type.Defense),
        new Move(Commands.UpDodge, Zone.Up, Power.Strong, Type.Defense),
        new Move(Commands.DownDodge, Zone.Down, Power.Strong, Type.Defense)
    };
    private Dictionary<Zone, float> zoneProbabilities = new Dictionary<Zone, float>();
    private Dictionary<Power, float> powerProbabilities = new Dictionary<Power, float>();
    private Dictionary<Type, float> typeProbabilities = new Dictionary<Type, float>();
    void Start()
    {
        zoneProbabilities.Add(Zone.Up, 5f / 9f);
        zoneProbabilities.Add(Zone.Down, 4f / 9f);
        powerProbabilities.Add(Power.Weak, 4f / 9f);
        powerProbabilities.Add(Power.Medium, 2f / 9f);
        powerProbabilities.Add(Power.Strong, 3f / 9f);
        typeProbabilities.Add(Type.Offense, 5f / 9f);
        typeProbabilities.Add(Type.Defense, 4f / 9f);
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
        Move moveUsed = Array.Find(moves, move => move.name == lastCommandUsed);
        Dictionary<Zone, float> newZoneProbas = new Dictionary<Zone, float>();
        Dictionary<Power, float> newPowerProbas = new Dictionary<Power, float>();
        Dictionary<Type, float> newTypeProbas = new Dictionary<Type, float>();
        foreach (KeyValuePair<Zone, float> zoneProba in zoneProbabilities)
        {
            if (zoneProba.Key == moveUsed.zone)
            {
                newZoneProbas.Add(zoneProba.Key, zoneProba.Value - (1f / 9f));
            }
            else
            {
                newZoneProbas.Add(zoneProba.Key, zoneProba.Value + (1f / 9f));
            }
        }
        foreach (KeyValuePair<Power, float> powerProba in powerProbabilities)
        {
            if (powerProba.Key == moveUsed.power)
            {
                newPowerProbas.Add(powerProba.Key, powerProba.Value - (1f / 9f));
            }
            else
            {
                newPowerProbas.Add(powerProba.Key, powerProba.Value + (1f / 9f));
            }
        }
        foreach (KeyValuePair<Type, float> typeProba in typeProbabilities)
        {
            if (typeProba.Key == moveUsed.type)
            {
                newTypeProbas.Add(typeProba.Key, typeProba.Value - (1f / 9f));
            }
            else
            {
                newTypeProbas.Add(typeProba.Key, typeProba.Value + (1f / 9f));
            }
        }
        zoneProbabilities = newZoneProbas;
        powerProbabilities = newPowerProbas;
        typeProbabilities = newTypeProbas;
    }

    private float GetMoveProbabilitySum(Move move)
    {
        return zoneProbabilities[move.zone] + powerProbabilities[move.power] + typeProbabilities[move.type];
    }
    private Move GetMove(Commands name)
    {
        return Array.Find(moves, (move => move.name == name));
    }
    private float GetCommandProbabilitySum(Commands name)
    {
        return GetMoveProbabilitySum(GetMove(name));
    }

    public Commands[] GetCommandsForCurrentRound()
    {
        // On appelle cette fonction pour recevoir les 3 commandes avec les plus gros taux de probabilité d'apparition. On gère pas le random bonus qui permet de ne pas être super
        // régulier sur les moves qui viennent.
        List<KeyValuePair<Commands, float>> sortedProbabilities = new List<KeyValuePair<Commands, float>>();

        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.UpJab,
            GetCommandProbabilitySum(Commands.UpJab)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.DownJab,
            GetCommandProbabilitySum(Commands.DownJab)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.UpCross,
            GetCommandProbabilitySum(Commands.UpCross)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.DownCross,
            GetCommandProbabilitySum(Commands.DownCross)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.Uppercut,
            GetCommandProbabilitySum(Commands.Uppercut)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.UpBlock,
            GetCommandProbabilitySum(Commands.UpBlock)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.DownBlock,
            GetCommandProbabilitySum(Commands.DownBlock)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.UpDodge,
            GetCommandProbabilitySum(Commands.UpDodge)
        ));
        sortedProbabilities.Add(new KeyValuePair<Commands, float>(
            Commands.DownDodge,
            GetCommandProbabilitySum(Commands.DownDodge)
        ));

        // sortedProbabilities.Sort((x, y) => x.Value.CompareTo(y.Value));
        sortedProbabilities = sortedProbabilities.OrderByDescending(proba => proba.Value).ToList();
        List<KeyValuePair<Commands, float>> finalList = sortedProbabilities.GetRange(0, 3);
        Commands[] returnedCommands = new Commands[3];

        for (int i = 0; i < finalList.Count; i++)
        {
            returnedCommands.SetValue(finalList[i].Key, i);
        }

        return returnedCommands;
    }
}
