using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {
    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    void OnTriggerEnter(Collider coin)
    {
        if (coin.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Debug.Log("Coin Picked");
        }
    }
}
