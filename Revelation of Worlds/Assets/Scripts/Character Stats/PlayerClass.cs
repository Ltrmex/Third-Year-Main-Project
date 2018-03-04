using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass {
    //  Variables
    private int strength;
    private int wisdom;
    private int agility;
    private int armor;
    private int hitPoints;
    private int regeneration;

    private string playerName;
    private int playerLevel;
    private BaseClass playerClass;

    public string PlayerName {
        get { return playerName; }
        set { playerName = value; }
    }

    public int PlayerLevel
    {
        get { return playerLevel; }
        set { playerLevel = value; }
    }

    public BaseClass PlayerC
    {
        get { return playerClass; }
        set { playerClass = value; }
    }

    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public int Wisdom
    {
        get { return wisdom; }
        set { wisdom = value; }
    }

    public int Agility
    {
        get { return agility; }
        set { agility = value; }
    }

    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    public int HitPoints
    {
        get { return hitPoints; }
        set { hitPoints = value; }
    }

    public int Regeneration
    {
        get { return regeneration; }
        set { regeneration = value; }
    }
}
