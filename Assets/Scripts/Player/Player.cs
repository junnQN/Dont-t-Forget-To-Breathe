using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
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
    public PlayerNoneState noneState { get; private set; }
    public PlayerDieState dieState { get; private set; }

    #endregion
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        holdBreatheState = new PlayerHoldBreatheState(this, stateMachine, "HoldBreathe");
        inhaleState = new PlayerInhaleState(this, stateMachine, "Inhale");
        exhaleState = new PlayerExhaleState(this, stateMachine, "Exhale");
        coughState = new PlayerCoughState(this, stateMachine, "Cough");
        noneState = new PlayerNoneState(this, stateMachine, "None");
        dieState = new PlayerDieState(this, stateMachine, "Die");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(noneState);
    }

    public void Init()
    {
        oxygen = 100f;
        carbonDioxide = 0f;
        stateMachine.ChangeState(holdBreatheState);
    }

    private void Update()
    {
        if (stateMachine.currentState != null)
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

    public void DecreaseOxygenOverTime()
    {
        var gameConfig = GameManager.instance.gameConfig;
        oxygen -= gameConfig.autoRate * Time.deltaTime;
    }

    public void IncreaseOxygenByInhale()
    {
        var gameConfig = GameManager.instance.gameConfig;
        oxygen += gameConfig.inhaleRate * Time.deltaTime;
    }

    public void IncreaseOxygenBySmoke()
    {
        var gameConfig = GameManager.instance.gameConfig;
        oxygen += gameConfig.amountAirBySmoke * Time.deltaTime;
    }

    public void IncreaseCarbonDioxideBySmoke()
    {
        var gameConfig = GameManager.instance.gameConfig;
        carbonDioxide += gameConfig.amountAirBySmoke * Time.deltaTime;
    }

    public void DecreaseCarbonDioxideByExhale()
    {
        var gameConfig = GameManager.instance.gameConfig;
        carbonDioxide -= gameConfig.exhaleRate * Time.deltaTime;
    }

    public void IncreaseCarbonDioxideOverTime()
    {
        var gameConfig = GameManager.instance.gameConfig;
        carbonDioxide += gameConfig.autoRate * Time.deltaTime;
    }

    public void DecreaseCarbonDioxideByCough()
    {
        var gameConfig = GameManager.instance.gameConfig;
        carbonDioxide -= gameConfig.amountCo_2Cough * Time.deltaTime;
    }
}


