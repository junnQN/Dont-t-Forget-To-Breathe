using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEatState : PlayerState
{
    public PlayerEatState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(6);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.instance.StopSFX(6);
    }

    public override void Update()
    {
        base.Update();

        if (!player.isDisableInput && Input.GetKey(KeyCode.I))
        {
            player.IncreaseAirByInhale();
            player.IncreaseCarbonDioxideOverTime();
        }

        else if (!player.isDisableInput && Input.GetKey(KeyCode.O))
        {
            player.DecreaseAirByExhale();
            player.DecreaseOxygenOverTime();
        }
        else
        {
            player.DecreaseOxygenOverTime();
            player.IncreaseCarbonDioxideOverTime();
        }

        if (xInput != 0)
            stateMachine.ChangeState(player.moveState);
    }
}
