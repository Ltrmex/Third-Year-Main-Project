using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author Cristina
public class VisibleTabs : MonoBehaviour {

    public GameObject tabsPanel;                                // The tab panel
    private bool isShowing = true;                              // Is the tab panel visible or not

    private void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // The tabs panel is enabled or disabled - enabled in this case - by pressing they "z" key on the keyboard
        // Once the tab is unpaused the game is unpaused
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
        // The tabs panel is enabled or disabled - disabled in this case - by pressing they "z" key on the keyboard
        // Once the tab panel is disabled the game is unpaused
        if (Input.GetKeyDown("z"))
        {
            tabsPanel.SetActive(!isShowing);
            Time.timeScale = 1;
        }

      
    }
}
