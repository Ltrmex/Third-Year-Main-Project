using UnityEngine.UI;

[System.Serializable]
public class User
{
    public Text levelDisplay;
    public Text enemiesDisplay;
    public Text scoreDisplay;

    private int level;
    private int enemies;
    private int score;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }   //  Name

    public int Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }   //  Difficulty

    public int Score
    {
        get { return score; }
        set { score = value; }
    }   //  Wave

    public void SetValues()
    {
        levelDisplay.text = Level.ToString();
        enemiesDisplay.text = Enemies.ToString();
        scoreDisplay.text = Score.ToString();
    }   //  SetValues()
}   //  User
