using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from :https://www.youtube.com/watch?v=6d7pmRE0T6c
namespace Ability
{
    public class PlayerStatus : MonoBehaviour
    {

        [Header("Main Player Stats")]
        public string PlayerName;
        public int PlayerHP = 50; //baseline = 50, +5 each level

        [SerializeField]
        private int m_PlayerXP = 0;
        public int PlayerXP
        {
            get { return m_PlayerXP; }
            set
            {
                m_PlayerXP = value;

                //If we have subscribers, then tell them xp changed :)
                if (onXPChange != null)
                    onXPChange();
            }
        }

        /* Set Listener for Player Level */
        [SerializeField]
        private int m_PlayerLevel = 1;
        public int PlayerLevel
        {
            get { return m_PlayerLevel; }
            set
            {
                m_PlayerLevel = value;

                //If we have subscribers, then tell them xp changed :)
                if (onLevelChange != null)
                    onLevelChange();
            }
        }


        [Header("Player Attributes")]
        public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

        [Header("Player Skills Enabled")]
        public List<Skills> PlayerSkills = new List<Skills>();

        //Delegates for listeners    
        public delegate void OnXPChange();
        public event OnXPChange onXPChange;

        public delegate void OnLevelChange();
        public event OnLevelChange onLevelChange;


        //Just some temporary methods to help test
        public void UpdateLevel(int amount)
        {
            PlayerLevel += amount;
        }
        public void UpdateXP(int amount)
        {
            PlayerXP += amount;
        }


    }
}
