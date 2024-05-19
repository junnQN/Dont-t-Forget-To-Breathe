using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerGroundState : PlayerState
{
    Tween counterTween;

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



        if (!player.isDisableInput && Input.GetKey(KeyCode.I))
        {
            if (player.isCold == true)
            {
                player.IncreaseOxygenByCold();
                player.IncreaseCarbonDioxideOverTime();
            }
            else
            {
                player.DecreaseTime(player.inhaleTime);
                player.IncreaseAirByInhale();
                player.IncreaseCarbonDioxideOverTime();
            }
        }

        else if (!player.isDisableInput && Input.GetKey(KeyCode.O))
        {
            if (player.isCold == true)
            {
                player.DecreaseCarbonDioxideByCold();
                player.DecreaseOxygenOverTime();
            }
            else
            {
                player.DecreaseExhaleTime();
                player.DecreaseAirByExhale();
                player.DecreaseOxygenOverTime();
            }

        }
        else
        {
            player.DecreaseOxygenOverTime();
            player.IncreaseCarbonDioxideOverTime();
        }

        if (!player.isDisableInput && Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

        if (!player.isDisableInput && Input.GetKeyDown(KeyCode.E) && player.isPlayerTouching && GameManager.instance.currentLevel != 3)
            stateMachine.ChangeState(player.eatState);

    }

    bool HandleInhaleSmoke()
    {
        double probability = GameManager.instance.gameConfig.coughRate;

        if (ShouldCallFunction(probability))
        {
            player.stateMachine.ChangeState(player.coughState);
            return true;
        }
        return false;
    }

    bool ShouldCallFunction(double probability)
    {
        double randomValue = Random.value;

        return randomValue < probability;
    }

}
