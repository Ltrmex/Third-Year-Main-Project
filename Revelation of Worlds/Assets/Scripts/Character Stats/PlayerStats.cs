﻿using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //  Variables
    private PlayerMovement playerMovement;
    private Player playerMovement2;
    private PlayerHealth playerHealth;
    private PlayerShooting playerShooting;

    //  Reference to gameobjects
    GameObject player;
    GameObject gun;
    GameObject mainC;

    // Use this for initialization
    void Start () {
        //  References
        player = GameObject.FindGameObjectWithTag("Player");
        gun = GameObject.FindGameObjectWithTag("Gun");
        mainC = GameObject.FindGameObjectWithTag("MainCamera");

        //  Setting values
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement2 = mainC.GetComponent<Player>();
        playerShooting = gun.GetComponent<PlayerShooting>();

    }
	
	// Update is called once per frame
	void Update () {
        //  Update values
        playerShooting.damagePerShot = CreatePlayer.newPlayer.AttackPower;
        playerShooting.timeBetweenBullets = CreatePlayer.newPlayer.AttackSpeed;

        playerHealth.startingHealth = CreatePlayer.newPlayer.Health;
        playerHealth.startingShieldPower = CreatePlayer.newPlayer.Shield;
        playerHealth.regeneration = CreatePlayer.newPlayer.Regeneration;

        playerMovement.speed = CreatePlayer.newPlayer.MovementSpeed;
        playerMovement2.moveSpeed = CreatePlayer.newPlayer.MovementSpeed;

    }
}
