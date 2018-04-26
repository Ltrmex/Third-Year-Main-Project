using UnityEngine;
using UnityEngine.UI;

public class CreatePlayer : MonoBehaviour {
    //  Variables
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
        coinsToSpend = coins.GetComponent<CoinSystem>();    //  reference to coins
        newPlayer = new PlayerClass();  //  create new player
    }

    //  Create new player
    public void CreateNewPlayer() {
        newPlayer.PlayerLevel = 1;
        newPlayer.PlayerName = playerName;

        //  Basic information
        GameInfo.PlayerLevel = newPlayer.PlayerLevel;
        GameInfo.PlayerName = newPlayer.PlayerName;
        GameInfo.PlayerC = newPlayer.PlayerC;

        //  Set attribute values
        GameInfo.AttackPower = newPlayer.AttackPower.ToString();
        GameInfo.Health = newPlayer.Health.ToString();
        GameInfo.Regeneration = newPlayer.Regeneration.ToString();
        GameInfo.Shield = newPlayer.Shield.ToString();
        GameInfo.MovementSpeed = newPlayer.MovementSpeed.ToString();
        GameInfo.AttackSpeed = newPlayer.AttackSpeed.ToString();
    }

    //  Set for speed
    public void SetSpeed() {
        newPlayer.PlayerC = new Speed();
        SetPoints();
    }

    //  Set for defence
    public void SetDefence() {
        newPlayer.PlayerC = new Defence();
        SetPoints();
    }

    //  Set for attack
    public void SetAttack() {
        newPlayer.PlayerC = new Attack();
        SetPoints();
    }

    //  Set points
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

    //  Set values
    public void SetAttackPower(int amount) {
        if (newPlayer.PlayerC != null) {
            if (amount > 0 && coinsToSpend.totalCoins > 10) {    //  check if coins available
                newPlayer.AttackPower += amount;    //  increase attack power
                coinsToSpend.totalCoins -= 10;
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
            if (amount > 0 && coinsToSpend.totalCoins > 10)
            {
                newPlayer.Health += amount;
                coinsToSpend.totalCoins -= 10;
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
            if (amount > 0 && coinsToSpend.totalCoins > 10)
            {
                newPlayer.Regeneration += amount;
                coinsToSpend.totalCoins -= 10;
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
            if (amount > 0 && coinsToSpend.totalCoins > 10)
            {
                newPlayer.Shield += amount;
                coinsToSpend.totalCoins -= 10;
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
            if (amount > 0 && coinsToSpend.totalCoins > 10)
            {
                newPlayer.MovementSpeed += amount;
                coinsToSpend.totalCoins -= 10;
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
            if (amount > 0 && coinsToSpend.totalCoins > 10)
            {
                newPlayer.AttackSpeed -= amount;
                coinsToSpend.totalCoins -= 10;
            }
            else
            {
                Debug.Log("No Class Chosen!!!");
            }
        }
    }
}
