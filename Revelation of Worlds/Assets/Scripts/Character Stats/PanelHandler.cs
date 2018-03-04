using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : MonoBehaviour {

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}
