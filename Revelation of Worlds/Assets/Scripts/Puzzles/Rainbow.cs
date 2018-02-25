using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour {

    public int digit = 0;
    static int[] solution = {1, 2, 3, 4, 5, 6, 7};
    private static int[] input = {-1, -1, -1, -1, -1, -1, -1};

    public float colourChangeDelay = 0.5f;
    float currentDelay = 0f;
    bool colourChangeCollision = false;
    Color32 color;

    void OnCollisionEnter(Collision other)
    {
        colourChangeCollision = true;
        currentDelay = Time.time + colourChangeDelay;
    }

    void checkColourChange()
    {
        switch (digit) {
            case 1:
                color = new Color32(255, 0, 0, 0);   //  red
                break;
            case 2:
                color = new Color32(255, 69, 0, 0); //  orange
                break;
            case 3:
                color = new Color32(255, 255, 0, 0);   //  yellow
                break;
            case 4:
                color = new Color32(0, 128, 0, 0);    //  green
                break;
            case 5:
                color = new Color32(0, 0, 255, 0); //  blue
                break;
            case 6:
                color = new Color32(75, 0, 130, 0); //  indigo
                break;
            case 7:
                color = new Color32(238, 130, 238, 0);    //  violet
                break;
        }

        if (colourChangeCollision)
        {
            this.transform.GetComponent<Renderer>().material.color = color;

            if (Time.time > currentDelay)
            {
                this.transform.GetComponent<Renderer>().material.color = Color.white;
                colourChangeCollision = false;
            }
        }
    }

    void Update()
    {
        checkColourChange();
    }
}
