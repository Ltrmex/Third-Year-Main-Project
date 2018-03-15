using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour {

    public static PlayerClass newPlayer;
    private string playerName = "Player 1";
    private CoinSystem coinsToSpend;

    //  UI
    public Text attackPowerText;
    public Text healthText;
    public Text regenerationText;
    public Text shieldText;
    public Text movementSpeedText;
    public Text attackSpeedText;
    public Text pointsText;
    GameObject coins;

    // Use this for initialization
    void Start () {
        coins = GameObject.FindGameObjectWithTag("Coins");
        coinsToSpend = coins.GetComponent<CoinSystem>();
        newPlayer = new PlayerClass();
	}

    public void CreateNewPlayer() {
        newPlayer.PlayerLevel = 1;
        newPlayer.PlayerName = playerName;

        GameInfo.PlayerLevel = newPlayer.PlayerLevel;
        GameInfo.PlayerName = newPlayer.PlayerName;
        GameInfo.PlayerC = newPlayer.PlayerC;

        GameInfo.AttackPower = newPlayer.AttackPower.ToString();
        GameInfo.Health = newPlayer.Health.ToString();
        GameInfo.Regeneration = newPlayer.Regeneration.ToString();
        GameInfo.Shield = newPlayer.Shield.ToString();
        GameInfo.MovementSpeed = newPlayer.MovementSpeed.ToString();
        GameInfo.AttackSpeed = newPlayer.AttackSpeed.ToString();
    }

    public void SetSpeed() {
        newPlayer.PlayerC = new Speed();
        SetPoints();
    }

    public void SetDefence() {
        newPlayer.PlayerC = new Defence();
        SetPoints();
    }

    public void SetAttack() {
        newPlayer.PlayerC = new Attack();
        SetPoints();
    }

    private void SetPoints() {
        newPlayer.AttackPower = newPlayer.PlayerC.AttackPower;
        newPlayer.Health = newPlayer.PlayerC.Health;
        newPlayer.Regeneration = newPlayer.PlayerC.Regeneration;
        newPlayer.Shield = newPlayer.PlayerC.Shield;
        newPlayer.MovementSpeed = newPlayer.PlayerC.MovementSpeed;
        newPlayer.AttackSpeed = newPlayer.PlayerC.AttackSpeed;
    }

    void Update() {
        float value = ((newPlayer.AttackSpeed * 10) - 10) * -1;

        attackPowerText.text = newPlayer.AttackPower.ToString();
        healthText.text = newPlayer.Health.ToString();
        regenerationText.text = newPlayer.Regeneration.ToString("F2");
        shieldText.text = newPlayer.Shield.ToString();
        movementSpeedText.text = newPlayer.MovementSpeed.ToString("F2") + "%";
        attackSpeedText.text = value.ToString("F2") + "%";

        pointsText.text = coinsToSpend.totalCoins.ToString();
    }

    public void SetAttackPower(int amount) {
        if (newPlayer.PlayerC != null) {
            if (amount > 0 && coinsToSpend.totalCoins > 0) {
                newPlayer.AttackPower += amount;
                --coinsToSpend.totalCoins;
            }
            else if (amount < 0 && newPlayer.AttackPower > newPlayer.PlayerC.AttackPower) {
                newPlayer.AttackPower += amount;
                ++coinsToSpend.totalCoins;
            }
            else {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetHealth(int amount)
    {
        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && coinsToSpend.totalCoins > 0)
            {
                newPlayer.Health += amount;
                --coinsToSpend.totalCoins;
            }
            else if (amount < 0 && newPlayer.Health > newPlayer.PlayerC.Health)
            {
                newPlayer.Health += amount;
                ++coinsToSpend.totalCoins;
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetRegeneration(int amount)
    {

        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && coinsToSpend.totalCoins > 0)
            {
                newPlayer.Regeneration += amount;
                --coinsToSpend.totalCoins;
            }
            else if (amount < 0 && newPlayer.Regeneration > newPlayer.PlayerC.Regeneration)
            {
                newPlayer.Regeneration += amount;
                ++coinsToSpend.totalCoins;
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetShield(int amount)
    {
        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && coinsToSpend.totalCoins > 0)
            {
                newPlayer.Shield += amount;
                --coinsToSpend.totalCoins;
            }
            else if (amount < 0 && newPlayer.Shield > newPlayer.PlayerC.Shield)
            {
                newPlayer.Shield += amount;
                ++coinsToSpend.totalCoins;
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetMovementSpeed(int num)
    {
        float amount = (float)num/8;

        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && coinsToSpend.totalCoins > 0)
            {
                newPlayer.MovementSpeed += amount;
                --coinsToSpend.totalCoins;
            }
            else if (amount < 0 && newPlayer.MovementSpeed > newPlayer.PlayerC.MovementSpeed)
            {
                newPlayer.MovementSpeed += amount;
                ++coinsToSpend.totalCoins;
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetAttackSpeed(int num)
    {
        float amount = (float)num/150;

        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && coinsToSpend.totalCoins > 0)
            {
                newPlayer.AttackSpeed += amount;
                --coinsToSpend.totalCoins;
            }
            else if (amount < 0)
            {
                newPlayer.AttackSpeed += amount;
                ++coinsToSpend.totalCoins;
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }
}
