using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass {
    //  Variables
    private int attackPower;
    private int health;
    private int regeneration;
    private int shield;
    private float movementSpeed;
    private float attackSpeed;

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

    public PlayerClass() {
        attackPower = 1;
        health = 1;
        regeneration = 0;
        shield = 1;
        movementSpeed = 1.0f;
        attackSpeed = 1.0f;
}

    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Regeneration
    {
        get { return regeneration; }
        set { regeneration = value; }
    }

    public int Shield
    {
        get { return shield; }
        set { shield = value; }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
}
