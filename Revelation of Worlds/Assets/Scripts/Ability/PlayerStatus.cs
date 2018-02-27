using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from :https://www.youtube.com/watch?v=6d7pmRE0T6c
namespace Ability
{
    public class PlayerStatus : MonoBehaviour
    {

        [Header("Main Player Status")]
        public string PlayerName;
        public int PlayerXP = 0;
        public int PayerLevel = 1;
        //Baselin = 50, +5each level
        public int PlayerHP = 50;

        [Header("Player Attributes")]
        public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
