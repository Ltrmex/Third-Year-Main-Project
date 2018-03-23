using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialiseStars : MonoBehaviour {
    // Level 1
    /* public GameObject L1star1;
     public GameObject L1star2;
     public GameObject L1star3;
     // Level 2
     public GameObject L2star1;
     public GameObject L2star2;
     public GameObject L2star3;
     // Level 3

     public GameObject L3star1;
     public GameObject L3star2;
     public GameObject L3star3;*/

    public GameObject Star0;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    // Use this for initialization
    void Start () {
        // Initialise them all to false
        // Level 1        
        /* L1star1.GetComponent<Image>().enabled = false;
         L1star2.GetComponent<Image>().enabled = false;
         L1star3.GetComponent<Image>().enabled = false;
         // Level 2
         L2star1.GetComponent<Image>().enabled = false;
         L2star2.GetComponent<Image>().enabled = false;
         L2star3.GetComponent<Image>().enabled = false;
         // Level 3
         L3star1.GetComponent<Image>().enabled = false;
         L3star2.GetComponent<Image>().enabled = false;
         L3star3.GetComponent<Image>().enabled = false;*/

        Star0.GetComponent<Image>().enabled = true;
        Star1.GetComponent<Image>().enabled = false;
        Star2.GetComponent<Image>().enabled = false;
        Star3.GetComponent<Image>().enabled = false;
    }

}
