using UnityEngine;


[CreateAssetMenu(fileName = "ConfigScripable", menuName = "Configs/Player", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [Header("Inhale/Exhale Configs")]
    [Tooltip("Amount of air inhaled")]
    public float oxygenRate = 10f;
    [Tooltip("Amount of air inhaled")]
    public float carbonDioxideRate = 10f;
}
