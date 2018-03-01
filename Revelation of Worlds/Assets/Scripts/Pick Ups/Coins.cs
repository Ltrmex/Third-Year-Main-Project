using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {
    private Color color;

    void OnCollisionEnter(Collision coin)
    {
        if (coin.gameObject.CompareTag("Player"))
        {
            // color = ()
            gameObject.SetActive(false);
            Debug.Log("Coin Picked");
        }
    }
}
