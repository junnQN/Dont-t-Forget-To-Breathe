using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] public float oxygen = 100f;
    [SerializeField] private float oxygenRate = 10f;
    [SerializeField] public float carbonDioxide;
    [SerializeField] private float carbonDioxideRate = 10f;

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


    public void DecreaseOxygen()
    {
        if (oxygen > 0)
        {
            oxygen -= oxygenRate * Time.deltaTime;
        }
    }

    public void DecreaseCarbonDioxide()
    {
        if (carbonDioxide > 0)
        {
            carbonDioxide -= carbonDioxideRate * Time.deltaTime;
        }
    }

    public void IncreaseOxygen()
    {
        if (oxygen < 100f)
        {
            oxygen += oxygenRate * Time.deltaTime;
        }
    }

    public void IncreaseCarbonDioxide()
    {
        if (carbonDioxide < 100f)
        {
            carbonDioxide += carbonDioxideRate * Time.deltaTime;
        }
    }
}


