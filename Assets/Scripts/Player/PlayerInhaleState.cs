using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerInhaleState : PlayerGroundState
{
    Tween counterTween;
    public PlayerInhaleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ChangeHoldBreatheStateStateAfterDelay();
    }

    private void ChangeHoldBreatheStateStateAfterDelay()
    {
        counterTween?.Kill();
        var time = GameManager.instance.gameConfig.maxTimeInhale;
        counterTween = DOVirtual.Float(0, 1, time, (v) => { })
        .OnComplete(() => player.stateMachine.ChangeState(player.idleState));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //<<<<<<< HEAD

        //player.IncreaseOxygen();
        //player.IncreaseCarbonDioxide();
        //=======
        if (GameManager.instance.isHaveSmoke())
        {
            if (HandleInhaleSmoke()) return;

            player.IncreaseCarbonDioxideBySmoke();
            player.IncreaseOxygenBySmoke();
        }
        else
        {
            player.IncreaseOxygenByInhale();
            player.IncreaseCarbonDioxideOverTime();
        }


        //>>>>>>> origin/quan
        if (Input.GetKeyUp(KeyCode.I))
        {
            player.stateMachine.ChangeState(player.idleState);
        }
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
