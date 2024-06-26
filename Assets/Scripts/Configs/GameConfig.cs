using UnityEngine;


[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
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

    [Tooltip("Amount of air inhaled when have smoke")]
    public float amountAirBySmoke = 10f;

    public float coughRate = 0.7f;

    [Tooltip("Max Time Inhale")]
    public float maxTimeInhale = 3f;

    [Tooltip("Max Time Exhale")]

    public float maxTimeExhale = 3f;

    [Header("Game Configs")]
    [Tooltip("The time of each level (seconds)")]
    public float[] timeOfLevels = new float[] { 60f, 120f, 180f, 240f, 300f };

    [Header("Water Configs")]
    public float waterMaxHeight = 2.5f;
    [Tooltip("The time to change water height (seconds)")]
    public float sprintTime = 2f;
    public float spawnTime = 2f;

    [Header("Smoke Configs")]
    public float timeAffectedBySmoke = 1f;
    public float smokeImmunityTime = 0.5f;

    [Header("Bomb Configs")]
    public float timeToExplode = 20f;
}
