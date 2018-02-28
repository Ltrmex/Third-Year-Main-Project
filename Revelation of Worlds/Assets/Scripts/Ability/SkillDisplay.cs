using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Kamila Michel
//Source code adapted from: https://www.youtube.com/watch?v=0WPBm9nGHdE

namespace Ability { 
    public class SkillDisplay : MonoBehaviour {

        //Get the Scriptable Object for Skills
        public Skills skill;
        //Get the UI componentes
        public Text skillName;
        public Text skillDescription;
        public Image skillIcon;
        public Text skillLevel;
        public Text skillXPNeeded;
        public Text skillAttributes;
        public Text skillAttrAmount;

        [SerializeField]
        private PlayerStatus m_PlayerHandler;

        // Use this for initialization
        void Start()
        {
            m_PlayerHandler = this.GetComponentInParent<PlayerHandler>().Player;
            //Listener for the XP change
            m_PlayerHandler.onXPChange += ReactToChange;
            //Listener for the Level change
            m_PlayerHandler.onLevelChange += ReactToChange;

            if (skill)
                //Calls SetValues from Skills class parsing true the game object and player handler
                skill.SetValues(this.gameObject, m_PlayerHandler);

            EnableSkills();
        }//End of Start method

        //Checks if player already has a skill
        public void EnableSkills()
        {
            //If the player has the skill already, then show it as enabled
            if (m_PlayerHandler && skill && skill.EnableSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }//If
            //If the player has the skill already, then show it as enabled
            else if (m_PlayerHandler && skill && skill.CheckSkills(m_PlayerHandler))
            {
                // If the player doesn't have the skill , then show it as disable
                this.GetComponent<Button>().interactable = true;
                this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
            }//Else if
            else
            {
                TurnOffSkillIcon();
            }//Else
        }//End of EnableSkills method

        private void OnEnable()
        {
            EnableSkills();
        }//End of OnEnable

        //Method to be used when user click the Skill icon
        public void GetSkill()
        {
            if (skill.GetSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
        }//End of GetSkill method

        //Turn on the Skill Icon - stop it from being clickable and disable the UI elements that make it change colour
        private void TurnOnSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
        }//End of TurnOnSkillIcon method

        //Turn off the Skill Icon so it cannot be used - stop it from being clickable and enable the UI elements that make it change colour
        private void TurnOffSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
        }//End of TurnOfSkillIcon method

        //event for when listener is woken
        void ReactToChange()
        {
            EnableSkills();
        }//End of ReactToChange method
    }//End of class SkillDisplay
}//End of namespace
