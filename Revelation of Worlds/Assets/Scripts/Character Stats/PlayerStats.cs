using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    PlayerMovement playerMovement;
    Player playerMovement2;
    PlayerHealth playerHealth;
    PlayerShooting playerShooting;

    GameObject player;
    GameObject gun;
    GameObject mainC;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gun = GameObject.FindGameObjectWithTag("Gun");
        mainC = GameObject.FindGameObjectWithTag("MainCamera");

        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement2 = mainC.GetComponent<Player>();
        playerShooting = gun.GetComponent<PlayerShooting>();

    }
	
	// Update is called once per frame
	void Update () {
        playerShooting.damagePerShot = CreatePlayer.newPlayer.AttackPower;
        playerShooting.timeBetweenBullets = CreatePlayer.newPlayer.AttackSpeed;

        playerHealth.startingHealth = CreatePlayer.newPlayer.Health;
        playerHealth.startingShieldPower = CreatePlayer.newPlayer.Shield;
        playerHealth.regeneration = CreatePlayer.newPlayer.Regeneration;

        playerMovement.speed = CreatePlayer.newPlayer.MovementSpeed;
        playerMovement2.moveSpeed = CreatePlayer.newPlayer.MovementSpeed;

    }
}
