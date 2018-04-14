using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleTabs : MonoBehaviour {

    public GameObject tabsPanel;
    private bool isShowing = true;

    private void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            isShowing = !isShowing;

            // If Time.timeScale has value 0 it means the game is paused otherwise it's not paused
            if (Time.timeScale == 1 && isShowing == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

            tabsPanel.SetActive(isShowing);
        }
    }

    private void exitTabs()
    {

        if (Input.GetKeyDown("z"))
        {
            tabsPanel.SetActive(!isShowing);
            Time.timeScale = 1;
        }

      
    }
}
