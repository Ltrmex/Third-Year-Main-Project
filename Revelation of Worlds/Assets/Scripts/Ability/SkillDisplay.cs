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
            //listener for the XP change
            m_PlayerHandler.onXPChange += ReactToChange;
            //listener for the Level change
            m_PlayerHandler.onLevelChange += ReactToChange;

            if (skill)
                skill.SetValues(this.gameObject, m_PlayerHandler);

            EnableSkills();
        }

        public void EnableSkills()
        {
            //if the player has the skill already, then show it as enabled
            if (m_PlayerHandler && skill && skill.EnableSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
            //if the player has the skill already, then show it as enabled
            else if (m_PlayerHandler && skill && skill.CheckSkills(m_PlayerHandler))
            {
                this.GetComponent<Button>().interactable = true;
                this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
            }
            else
            {
                TurnOffSkillIcon();
            }
        }

        private void OnEnable()
        {
            EnableSkills();
        }

        //Method to be used when you click the Skill icon
        public void GetSkill()
        {
            if (skill.GetSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
        }

        //Turn on the Skill Icon - stop it from being clickable and disable the UI elements that make it change colour
        private void TurnOnSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
        }

        //Turn off the Skill Icon so it cannot be used - stop it from being clickable and enable the UI elements that make it change colour
        private void TurnOffSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
        }

        //event for when listener is woken
        void ReactToChange()
        {
            EnableSkills();
        }

    }
}
