using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour {

    private PlayerClass newPlayer;
    private string playerName = "Player 1";

    //  UI
    public Text strengthText;
    public Text wisdomText;
    public Text agilityText;
    public Text armorText;
    public Text hitPointsText;
    public Text regenerationText;
    public Text pointsText;
    private int pointsToSpend = 20;

	// Use this for initialization
	void Start () {
        newPlayer = new PlayerClass();
        UpdateUI();
	}

    public void CreateNewPlayer() {
        newPlayer.PlayerLevel = 1;
        newPlayer.PlayerName = playerName;

        GameInfo.PlayerLevel = newPlayer.PlayerLevel;
        GameInfo.PlayerName = newPlayer.PlayerName;
        GameInfo.PlayerC = newPlayer.PlayerC;

        GameInfo.Strength = newPlayer.Strength.ToString();
        GameInfo.Wisdom = newPlayer.Wisdom.ToString();
        GameInfo.Agility = newPlayer.Agility.ToString();
        GameInfo.Armor = newPlayer.Armor.ToString();
        GameInfo.HitPoints = newPlayer.HitPoints.ToString();
        GameInfo.Regeneration = newPlayer.Regeneration.ToString();
    }

    public void SetArcherClass() {
        pointsToSpend = 20;
        newPlayer.PlayerC = new ArcherClass();
        SetPoints();
        //  Update UI
        UpdateUI();
    }

    public void SetWarriorClass() {
        pointsToSpend = 20;
        newPlayer.PlayerC = new WarriorClass();
        SetPoints();
        //  Update UI
        UpdateUI();
    }

    public void SetMageClass() {
        pointsToSpend = 20;
        newPlayer.PlayerC = new MageClass();
        SetPoints();
        //  Update UI
        UpdateUI();
    }

    private void SetPoints() {
        newPlayer.Strength = newPlayer.PlayerC.Strength;
        newPlayer.Wisdom = newPlayer.PlayerC.Wisdom;
        newPlayer.Agility = newPlayer.PlayerC.Agility;
        newPlayer.Armor = newPlayer.PlayerC.Armor;
        newPlayer.HitPoints = newPlayer.PlayerC.HitPoints;
        newPlayer.Regeneration = newPlayer.PlayerC.Regeneration;
    }

    void UpdateUI() {
        strengthText.text = newPlayer.Strength.ToString();
        wisdomText.text = newPlayer.Wisdom.ToString();
        agilityText.text = newPlayer.Agility.ToString();
        armorText.text = newPlayer.Armor.ToString();
        hitPointsText.text = newPlayer.HitPoints.ToString();
        regenerationText.text = newPlayer.Regeneration.ToString();

        pointsText.text = pointsToSpend.ToString();
    }

    public void SetStrength(int amount) {
        if (newPlayer.PlayerC != null) {
            if (amount > 0 && pointsToSpend > 0) {
                newPlayer.Strength += amount;
                --pointsToSpend;
                UpdateUI();
            }
            else if (amount < 0 && newPlayer.Strength > newPlayer.PlayerC.Strength) {
                newPlayer.Strength += amount;
                ++pointsToSpend;
                UpdateUI();
            }
            else {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetWisdom(int amount)
    {
        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Wisdom += amount;
                --pointsToSpend;
                UpdateUI();
            }
            else if (amount < 0 && newPlayer.Wisdom > newPlayer.PlayerC.Wisdom)
            {
                newPlayer.Wisdom += amount;
                ++pointsToSpend;
                UpdateUI();
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetAgility(int amount)
    {
        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Agility += amount;
                --pointsToSpend;
                UpdateUI();
            }
            else if (amount < 0 && newPlayer.Agility > newPlayer.PlayerC.Agility)
            {
                newPlayer.Agility += amount;
                ++pointsToSpend;
                UpdateUI();
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetArmor(int amount)
    {
        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Armor += amount;
                --pointsToSpend;
                UpdateUI();
            }
            else if (amount < 0 && newPlayer.Armor > newPlayer.PlayerC.Armor)
            {
                newPlayer.Armor += amount;
                ++pointsToSpend;
                UpdateUI();
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }

    public void SetHitPoints(int amount)
    {
        if (newPlayer.PlayerC != null)
        {
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.HitPoints += amount;
                --pointsToSpend;
                UpdateUI();
            }
            else if (amount < 0 && newPlayer.HitPoints > newPlayer.PlayerC.HitPoints)
            {
                newPlayer.HitPoints += amount;
                ++pointsToSpend;
                UpdateUI();
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
            if (amount > 0 && pointsToSpend > 0)
            {
                newPlayer.Regeneration += amount;
                --pointsToSpend;
                UpdateUI();
            }
            else if (amount < 0 && newPlayer.Regeneration > newPlayer.PlayerC.Regeneration)
            {
                newPlayer.Regeneration += amount;
                ++pointsToSpend;
                UpdateUI();
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }
}
