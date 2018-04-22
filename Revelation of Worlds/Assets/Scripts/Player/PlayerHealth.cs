﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //  Variables
    public int startingHealth = 1;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public int startingShieldPower;
    public int currentShieldPower;
    public Text shieldDisplay;
    public Text healthDisplay;
    public int regeneration = 0;
    public Slider shieldSlider;
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
    PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    public bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
    }


    private void Start() {
        InvokeRepeating("Heal", 0, 0.5f);   //  regeneration
    }

    void Heal() {
        currentHealth += regeneration;
    }

    //  Update values
    public void ChangeValue() {
        currentHealth = CreatePlayer.newPlayer.Health;
        currentShieldPower = CreatePlayer.newPlayer.Shield;
    }

    void Update()
    {
        startingHealth = CreatePlayer.newPlayer.Health;
        startingShieldPower = CreatePlayer.newPlayer.Shield;

        if (startingHealth > 100)
            CancelInvoke("Heal");

        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;

        if (currentShieldPower <= 0)
            shieldDisplay.text = "Shield: 0";
        else
            shieldDisplay.text = "Shield: " + currentShieldPower;

        if (currentHealth <= 0)
            healthDisplay.text = "Health: 0";
        else
            healthDisplay.text = "Health: " + currentHealth;

    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        if(currentShieldPower > 0) { 
            currentShieldPower -= (amount + 10);
            shieldSlider.value = currentShieldPower;
        }
        else { 
            currentHealth -= amount;    // reduce the current health by the damage amount.
            healthSlider.value = currentHealth; // set the health bar's value to the current health.
        }

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}