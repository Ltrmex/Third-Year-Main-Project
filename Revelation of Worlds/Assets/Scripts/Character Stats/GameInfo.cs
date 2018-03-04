using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public static string PlayerName { get; set; }
    public static int PlayerLevel { get; set; }
    public static BaseClass PlayerC { get; set; }

    public static string Strength { get; set; }
    public static string Wisdom { get; set; }
    public static string Agility { get; set; }
    public static string Armor { get; set; }
    public static string HitPoints { get; set; }
    public static string Regeneration { get; set; }
}
