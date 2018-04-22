using UnityEngine;

//  Game stats information
public class GameInfo : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public static string PlayerName { get; set; }
    public static int PlayerLevel { get; set; }
    public static BaseClass PlayerC { get; set; }

    public static string AttackPower { get; set; }
    public static string Health { get; set; }
    public static string Regeneration { get; set; }
    public static string Shield { get; set; }
    public static string MovementSpeed { get; set; }
    public static string AttackSpeed { get; set; }
}
