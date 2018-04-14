using UnityEngine;

public class Highscore : MonoBehaviour {
    public User[] user;
    private int i = 0;
    private DataController dataController;
    private Data[] data;

    // Use this for initialization
    void Start() {
        dataController = FindObjectOfType<DataController>();
        data = dataController.Get();

        while (i < user.Length)
        {
            user[i].Level = data[i].level;
            user[i].Enemies = data[i].enemies;
            user[i].Score = data[i].score;
            user[i].SetValues();

            ++i;
        }   //  for

    }   //  Start()

}   //  Highscores
