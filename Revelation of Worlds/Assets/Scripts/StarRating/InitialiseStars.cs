using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialiseStars : MonoBehaviour {
    public GameObject Star0;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    // Use this for initialization
    void Start () {
       
        Star0.GetComponent<Image>().enabled = true;
        Star1.GetComponent<Image>().enabled = false;
        Star2.GetComponent<Image>().enabled = false;
        Star3.GetComponent<Image>().enabled = false;
        
    }

}
