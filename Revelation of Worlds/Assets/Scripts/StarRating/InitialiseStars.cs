using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author Cristina
public class InitialiseStars : MonoBehaviour {
    public GameObject Star0;                                                    // Variable holding image with no stars
    public GameObject Star1;                                                    // Variable holding image with 1 star
    public GameObject Star2;                                                    // Variable holding image with 2 stars
    public GameObject Star3;                                                    // Variable holding image with 2 stars

    // Use this for initialization
    void Start () {
        // Image with no star is enabled and the other 3 are disabled - only the image with no stars shows
        Star0.GetComponent<Image>().enabled = true;
        Star1.GetComponent<Image>().enabled = false;
        Star2.GetComponent<Image>().enabled = false;
        Star3.GetComponent<Image>().enabled = false;
        
    }

}
