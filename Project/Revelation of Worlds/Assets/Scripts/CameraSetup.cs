using UnityEngine;
using System.Collections;

/* 
Author: Maciej Majchrzak
Student Number: G00332746
Date: 06-02-2018
Tutorial Reference: https://unity3d.com/learn/tutorials/s/survival-shooter-tutorial
NOTE: Code was developed with the aid of the tutorial mentioned above     
*/

public class CameraSetup : MonoBehaviour {
    public Transform target;    // position to follow
    public float smoothing = 5f;    // speed of the camera

    Vector3 offset; // offset

    // Start function
    void Start() {
        offset = transform.position - target.position;  // calculate offset
    }   // Start

    // Update function 
    void FixedUpdate() {
        Vector3 targetCamPos = target.position + offset;    // position camera is pointing on

        // Smoothly interpolate between the camera's current position and it's target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    } // FixedUpdate

}   // CameraSetup