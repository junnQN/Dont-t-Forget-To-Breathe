using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [Header("Inhale/Exhale Configs")]
    [Tooltip("Amount of air inhaled")]
    public float inhaleRate = 10f;
    [Tooltip("Amount of air exhaled")]
    public float exhaleRate = 10f;
    [Tooltip("Air volume changes over time")]
    public float autoRate = 10f;
    [Tooltip("The amount of co2 decreases when coughing")]
    public float amountCo_2Cough = 10f;
    [Tooltip("The time to change state after coughing (seconds)")]
    public float timeToBreakCoughState = 1f;

    [Header("Game Configs")]
    [Tooltip("The time of each level (seconds)")]
    public float[] timeOfLevels = new float[] { 60f, 120f, 180f, 240f, 300f };
}
