using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Kamila Michel
//Source code adapted from: https://www.youtube.com/watch?v=6d7pmRE0T6c
namespace Ability
{
    [CreateAssetMenu(menuName = "RPG Generator/Player/Create Attributes")]
    public class Attributes : ScriptableObject
    {

        public string Description;
        public Sprite Thumbnail;
    }
}
