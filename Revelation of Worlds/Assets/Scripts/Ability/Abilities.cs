using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {
    public GameObject[] abilities;
    public Button referenceToTheButton;

    private void Update()
    {
        if (Input.GetKeyDown("1"))
            FirstSkill();
        else if (Input.GetKeyDown("2"))
            SecondSkill();
        else if (Input.GetKeyDown("3"))
            ThirdSkill();
        else if (Input.GetKeyDown("4"))
            FourthSkill();
        else if (Input.GetKeyDown("5"))
            FifthSkill();
        else if (Input.GetKeyDown("6"))
            SixthSkill();

    }

    private void Start()
    {
        abilities = GameObject.FindGameObjectsWithTag("Abilities");
    }

    IEnumerator ExecuteAfterTime(float time, int num, int stat)
    {
        Image gameObjectRenderer = abilities[num].GetComponent<Image>();

        gameObjectRenderer.color = Color.red;
        yield return new WaitForSeconds(time);
        gameObjectRenderer.color = Color.white;

        referenceToTheButton.onClick.Invoke();
    }

    public void FirstSkill()
    {
        ++CreatePlayer.newPlayer.Health;
        referenceToTheButton.onClick.Invoke();

        StartCoroutine(ExecuteAfterTime(10, 0, --CreatePlayer.newPlayer.Health));
    }

    public void SecondSkill()
    {
        ++CreatePlayer.newPlayer.AttackPower;
        StartCoroutine(ExecuteAfterTime(10, 2, --CreatePlayer.newPlayer.AttackPower));
    }

    public void ThirdSkill()
    {
        ++CreatePlayer.newPlayer.Shield;
        referenceToTheButton.onClick.Invoke();
        StartCoroutine(ExecuteAfterTime(10, 3, --CreatePlayer.newPlayer.Shield));
    }

    public void FourthSkill()
    {
        ++CreatePlayer.newPlayer.Health;

        StartCoroutine(ExecuteAfterTime(20, 1, CreatePlayer.newPlayer.Health -= 5));
        referenceToTheButton.onClick.Invoke();
    }

    public void FifthSkill()
    {
        CreatePlayer.newPlayer.AttackPower += 5;
        referenceToTheButton.onClick.Invoke();

        
        StartCoroutine(ExecuteAfterTime(20, 4, CreatePlayer.newPlayer.AttackPower -= 5));
    }

    public void SixthSkill()
    {
        CreatePlayer.newPlayer.Shield += 5;
        StartCoroutine(ExecuteAfterTime(20, 1, CreatePlayer.newPlayer.Shield -= 5));

    }
}
