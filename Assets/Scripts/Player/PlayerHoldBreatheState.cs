using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldBreatheState : PlayerGroundState
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
        
        player.IncreaseCarbonDioxide();
        player.DecreaseOxygen();
        if(Input.GetKey(KeyCode.I))
            player.stateMachine.ChangeState(player.inhaleState);
        else if (Input.GetKey(KeyCode.O))
            player.stateMachine.ChangeState(player.exhaleState);
        
    }
}
