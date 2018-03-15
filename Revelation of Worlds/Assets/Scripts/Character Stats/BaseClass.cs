using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass {
    //  Variables
    private int attackPower;
    private int health;
    private int regeneration;
    private int shield;
    private float movementSpeed;
    private float attackSpeed;

    public int AttackPower {
        get { return attackPower; }
        set { attackPower = value;  }
    }

    public int Health {
        get { return health; }
        set { health = value; }
    }

    public int Regeneration {
        get { return regeneration; }
        set { regeneration = value; }
    }

    public int Shield {
        get { return shield; }
        set { shield = value; }
    }

    public float MovementSpeed {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float AttackSpeed {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
}
