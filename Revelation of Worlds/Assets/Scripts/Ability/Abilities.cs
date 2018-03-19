using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour {
    public GameObject[] abilities;

    private void Update()
    {
        if (Input.GetKeyDown("1"))
            firstSkill();
        else if (Input.GetKeyDown("2"))
            secondSkill();
        else if (Input.GetKeyDown("3"))
            thirdSkill();
        else if (Input.GetKeyDown("4"))
            fourthSkill();
        else if (Input.GetKeyDown("5"))
            fifthSkill();
        else if (Input.GetKeyDown("6"))
            sixthSkill();

    }

    private void Start()
    {

    }

    IEnumerator ExecuteAfterTime(float time, int num)
    {
        abilities = GameObject.FindGameObjectsWithTag("Abilities");
        Image gameObjectRenderer = abilities[num].GetComponent<Image>();
        gameObjectRenderer.color = Color.red;
        yield return new WaitForSeconds(time);
        gameObjectRenderer.color = Color.white;
    }

    public void firstSkill()
    {
        Debug.Log("Skill 1");
        StartCoroutine(ExecuteAfterTime(1, 5));
    }

    public void secondSkill()
    {
        Debug.Log("Skill 2");
        StartCoroutine(ExecuteAfterTime(1, 4));
    }

    public void thirdSkill()
    {
        Debug.Log("Skill 3");
        StartCoroutine(ExecuteAfterTime(1, 3));
    }

    public void fourthSkill()
    {
        Debug.Log("Skill 4");
        StartCoroutine(ExecuteAfterTime(1, 2));
    }

    public void fifthSkill()
    {
        Debug.Log("Skill 5");
        StartCoroutine(ExecuteAfterTime(1, 1));
    }

    public void sixthSkill()
    {
        Debug.Log("Skill 6");
        StartCoroutine(ExecuteAfterTime(1, 0));
    }
}
