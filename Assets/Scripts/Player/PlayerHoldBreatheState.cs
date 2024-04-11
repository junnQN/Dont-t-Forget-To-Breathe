using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldBreatheState : PlayerState
{
    public PlayerHoldBreatheState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.DecreaseOxygenOverTime();
        player.IncreaseCarbonDioxideOverTime();

        if (Input.GetKeyDown(KeyCode.I))
            player.stateMachine.ChangeState(player.inhaleState);
        else if (Input.GetKeyDown(KeyCode.O))
            player.stateMachine.ChangeState(player.exhaleState);
    }

    void HandleInhaleSmoke()
    {
        double probability = 0.7;

        if (ShouldCallFunction(probability))
        {
            player.stateMachine.ChangeState(player.coughState);
        }
        else
        {
            player.stateMachine.ChangeState(player.inhaleState);
        }
    }

    bool ShouldCallFunction(double probability)
    {
        double randomValue = Random.value;

        return randomValue < probability;
    }
}
