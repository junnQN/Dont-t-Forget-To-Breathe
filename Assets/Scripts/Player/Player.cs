using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] public PlayerConfig playerConfig;
    [SerializeField] public float oxygen = 100f;
    [SerializeField] public float carbonDioxide;

    #region Components
    public Animator anim { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerInhaleState inhaleState { get; private set; }
    public PlayerExhaleState exhaleState { get; private set; }
    public PlayerHoldBreatheState holdBreatheState { get; private set; }
    public PlayerCoughState coughState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        holdBreatheState = new PlayerHoldBreatheState(this, stateMachine, "HoldBreathe");
        inhaleState = new PlayerInhaleState(this, stateMachine, "Inhale");
        exhaleState = new PlayerExhaleState(this, stateMachine, "Exhale");
        coughState = new PlayerCoughState(this, stateMachine, "Cough");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(holdBreatheState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void ChangeOxygen(float amount)
    {
        oxygen += amount;
    }

    public void ChangeCarbonDioxide(float amount)
    {
        carbonDioxide += amount;
    }

    // public void DecreaseOxygen()
    // {
    //     oxygen -= playerConfig.inhaleRate * Time.deltaTime;
    // }

    // public void IncreaseOxygenByInhale()
    // {
    //     oxygen += playerConfig.inhaleRate * Time.deltaTime;
    // }

    // public void DecreaseCarbonDioxideByExhale()
    // {
    //     carbonDioxide -= playerConfig.autoRate * Time.deltaTime;
    // }

    // public void IncreaseCarbonDioxideByTime()
    // {
    //     carbonDioxide += playerConfig.autoRate * Time.deltaTime;
    // }
}


