using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from: https://www.youtube.com/watch?v=28JTTXqMvOU

public class MiniMapScript : MonoBehaviour {

    public Transform player;

    private void LateUpdate()
    {
        //Follows the position of the player
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //Map rotate with the player 90degree
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
