using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(7);
        player.anim.SetBool("die", true);
        
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(7);
        player.anim.SetBool("die", false);
    }

    public override void Update()
    {
        base.Update();
    }
}
