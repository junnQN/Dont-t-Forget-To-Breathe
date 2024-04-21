using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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



        if (Input.GetKey(KeyCode.I))
        {
            player.IncreaseOxygenByInhale();
            player.IncreaseCarbonDioxideOverTime();
        }

        else if (Input.GetKey(KeyCode.O))
        {
            player.DecreaseCarbonDioxideByExhale();
            player.DecreaseOxygenOverTime();
        }
        else
        {
            player.DecreaseOxygenOverTime();
            player.IncreaseCarbonDioxideOverTime();   
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
        
        if(Input.GetKeyDown(KeyCode.E)&&player.isPlayerTouching)
            stateMachine.ChangeState(player.eatState);
        
    }
}
