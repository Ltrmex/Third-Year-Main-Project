using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour {

    public int digit = 0;
    public GameObject[] buttons;

    static int[] solution = {1, 2, 3, 4, 5, 6, 7};

    private Color32 color;
    private static int pressCount = 0;
    private static int[] input = { -1, -1, -1, -1, -1, -1, -1 };
    

    void OnMouseDown() {
        determineColor();

        for (int i = 0; i < 6; i++)
            input[i] = input[i + 1];

        input[6] = digit;

        transform.GetComponent<Renderer>().material.color = color;

        checkSolution();
    }

    void checkSolution() {
        ++pressCount;

        if (solution[0] == input[0] && solution[1] == input[1] && solution[2] == input[2] && solution[3] == input[3] && solution[4] == input[4] && solution[5] == input[5] && solution[6] == input[6]) {
            Debug.Log("Correct Answer");
        }

        else if(pressCount == 7){
            for (int i = 0; i < 7; i++) {
                buttons[i].transform.GetComponent<Renderer>().material.color = Color.white;
                input[i] = -1;
            }

            pressCount = 0;
            Debug.Log("Wrong Answer");
        }
    }

    void determineColor() {
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
    }

    void Start() {
        buttons = GameObject.FindGameObjectsWithTag("RainbowPuzzle");
    }
}
