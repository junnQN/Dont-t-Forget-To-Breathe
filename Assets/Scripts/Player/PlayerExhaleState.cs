using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerExhaleState : PlayerGroundState
{
    Tween counterTween;

    public PlayerExhaleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        var time = GameManager.instance.gameConfig.maxTimeExhale;
//<<<<<<< HEAD
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState(player.idleState);
//=======
        counterTween = DOVirtual.Float(0, 1, time, (v) => { })
        .OnComplete(() => player.stateMachine.ChangeState(player.holdBreatheState));
//>>>>>>> origin/quan
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.DecreaseCarbonDioxideByExhale();
        player.DecreaseOxygenOverTime();
        if (Input.GetKeyUp(KeyCode.O))
            player.stateMachine.ChangeState(player.idleState);
    }
}
