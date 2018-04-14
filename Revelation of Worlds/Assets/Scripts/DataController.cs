using System;
using UnityEngine;

//  CODE ADAPTED FROM: https://unity3d.com/learn/tutorials/topics/scripting/high-score-playerprefs

public class DataController : MonoBehaviour
{
    private Data[] data = new Data[4];
    private bool isSaved = false;

    private void Start()
    {
        isSaved = false;
        Load();
    }   //  Start()

    public void Submit(int newLevel, int newEnemies, int newScore)
    {
        for (int i = 0; i < 4; i++)
        {
            if (newScore > data[i].score)
            {
                data[i].level = newLevel;
                data[i].enemies = newEnemies;
                data[i].score = newScore;

                Array.Sort(data, delegate (Data x, Data y) { return x.score.CompareTo(y.score); });

                if (!isSaved)
                    Save();
                else
                    return;
            }   //  if
        }   //  for
    }   //  Submit()

    public Data[] Get()
    {
        return data;
    }   //  Get()

    private void Load()
    {
        for (int i = 0; i < 4; i++)
            data[i] = new Data();

        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + (i + 1)))
            {
                data[i].score = PlayerPrefs.GetInt("HighScore" + (i + 1));
                data[i].level = PlayerPrefs.GetInt("Level" + (i + 1));
                data[i].enemies = PlayerPrefs.GetInt("Enemies" + (i + 1));
            }   //  if
        }   //  for
    }   //  Load()

    private void Save()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("HighScore" + (i + 1), data[i].score);
            PlayerPrefs.SetInt("Level" + (i + 1), data[i].level);
            PlayerPrefs.SetInt("Enemies" + (i + 1), data[i].enemies);
            isSaved = true;
        }   //  for
    }   //  Save()
}   //  DataController
