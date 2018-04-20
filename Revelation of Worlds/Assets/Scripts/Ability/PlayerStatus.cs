using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from :https://www.youtube.com/watch?v=6d7pmRE0T6c
namespace Ability
{
    public class PlayerStatus : MonoBehaviour
    {
        //private LevelSystem lvl;
        GameObject experienceObject;

        [Header("Main Player Stats")]
        public string PlayerName;
        public int PlayerHP = 50; //baseline = 50, +5 each level

        [SerializeField]
        private int m_PlayerXP = 0;


        private void Start()
        {
            experienceObject = GameObject.FindGameObjectWithTag("EXP");
            //lvl = experienceObject.GetComponent<LevelSystem>();
        }

        public int PlayerXP
        {
            get { return m_PlayerXP; }
            set
            {
                m_PlayerXP = value;

                //If we have subscribers, then tell user that the xp changed
                if (onXPChange != null)
                    onXPChange();
            }
        }//End of PlayerXp method

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
        }//End of PlayerLevel

        private void Update()
        {
            m_PlayerXP = LevelSystem.experiencePoints;
            m_PlayerLevel = LevelSystem.currentLevel;
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
        }//End of UpdateLevel method
        public void UpdateXP(int amount)
        {
            PlayerXP += amount;
        }//End of UpdateXp method
    }//End od PlayerStatus class
}//End of namespace
