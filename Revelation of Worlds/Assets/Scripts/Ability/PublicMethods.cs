using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from: https://www.youtube.com/watch?v=6d7pmRE0T6c

namespace Ability {

    [System.Serializable]
    public class PlayerAttributes
    {
        public Attributes attribute;
        public int amount;

        //Constructor
        public PlayerAttributes(Attributes attribute, int amount)
        {
            this.attribute = attribute;
            this.amount = amount;
        }
    }
}
