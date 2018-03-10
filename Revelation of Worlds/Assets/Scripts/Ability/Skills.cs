using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from: https://www.youtube.com/watch?v=6d7pmRE0T6c

namespace Ability
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public int LevelNeeded;
        public int XPNeeded;

        public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();

        //Public Method to set the values in the Skills UI (Populate the UI)
        public void SetValues(GameObject SkillDisplayObject, PlayerStatus Player)
        {
            //Error handling if they are not set it won't populate it, but if they are set will populate it
            if (SkillDisplayObject)
            {
                SkillDisplay SD = SkillDisplayObject.GetComponent<SkillDisplay>();
                SD.skillName.text = name;
                if (SD.skillDescription)
                    SD.skillDescription.text = Description;

                if (SD.skillIcon)
                    SD.skillIcon.sprite = Icon;

                if (SD.skillLevel)
                    SD.skillLevel.text = LevelNeeded.ToString();

                if (SD.skillXPNeeded)
                    SD.skillXPNeeded.text = XPNeeded.ToString() + "XP";

                if (SD.skillAttributes)
                    SD.skillAttributes.text = AffectedAttributes[0].attribute.ToString();

                if (SD.skillAttrAmount)
                    SD.skillAttrAmount.text = "+" + AffectedAttributes[0].amount.ToString();
            }//End of if statement
        }//End of SetValue method

        //Checks if the player is able to get the skill(Checks if has enough level and XP)
        public bool CheckSkills(PlayerStatus Player)
        {
            //Checks if player is the right level
            if (Player.PlayerLevel < LevelNeeded)
                return false;

            //Checks if player has enough xp
            if (Player.PlayerXP < XPNeeded)
                return false;

            //otherwise they can enable this skill
            return true;
        }//End of CheckSkills method

        //Checks if the player already has the skill
        public bool EnableSkill(PlayerStatus Player)
        {
            //Goes through all the skills that the player currently has
            List<Skills>.Enumerator skills = Player.PlayerSkills.GetEnumerator();
            while (skills.MoveNext())
            {
                var CurrSkill = skills.Current;
                if (CurrSkill.name == this.name)
                {
                    return true;
                }
            }//End of while loop
            return false;
        }//End of EnableSkills method

        //Gets new skill
        public bool GetSkill(PlayerStatus Player)
        {
            int i = 0;
            //List through the Skill's Attributes
            List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
            while (attributes.MoveNext())
            {
                //List through the Players Attributes and match with Skill attribute
                List<PlayerAttributes>.Enumerator PlayerAttr = Player.Attributes.GetEnumerator();
                while (PlayerAttr.MoveNext())
                {
                    if (attributes.Current.attribute.name.ToString() == PlayerAttr.Current.attribute.name.ToString())
                    {
                        //update the players attributes
                        PlayerAttr.Current.amount += attributes.Current.amount;
                        //mark that an attribute was updated
                        i++;
                    }//Enf of if
                }//End of while
                //If found attribute
                if (i > 0)
                {
                    //Reduces the XP from  the player
                    LevelSystem.experiencePoints -= this.XPNeeded;

                    //Adds to the list of skills
                    Player.PlayerSkills.Add(this);
                    return true;
                }//End of if
            }//End of while loop
            return false;
        }//End of SetValues method
    }//End of Skills class
}//End of namespace

