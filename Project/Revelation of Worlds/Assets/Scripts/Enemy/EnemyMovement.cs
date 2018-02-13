using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Author: Kamila Michel
//Student: G00340498
//Code adapted from: https://unity3d.com/learn/tutorials/projects/survival-shooter/enemy-one?playlist=17144

public class EnemyMovement : MonoBehaviour {

    //Potision of the player
    Transform player;               
    
    //Will be use later when setting up the health option for player and enemy
    PlayerHealth playerHealth;     
    EnemyHealth enemyHealth; 
    
    // Reference to the nav mesh agent
    NavMeshAgent nav;               


    void Awake()
    {
        // Set up the references
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Part of health code
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();


        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //Sets the destination of the nav mesh agent to the player
        nav.SetDestination(player.position);
       
    }
}
