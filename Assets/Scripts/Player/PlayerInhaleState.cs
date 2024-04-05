using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInhaleState : PlayerState
{
    public PlayerInhaleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        player.IncreaseOxygen();
        player.IncreaseCarbonDioxide();
        if (Input.GetKeyUp(KeyCode.I))
        {
            player.stateMachine.ChangeState(player.holdBreatheState);
        }
        else if (Input.GetKey(KeyCode.O))
        {
            player.stateMachine.ChangeState(player.exhaleState);
        }
    }
}
