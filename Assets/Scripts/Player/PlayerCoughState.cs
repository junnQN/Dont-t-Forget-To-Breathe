using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoughState : PlayerState
{
    public PlayerCoughState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.StartCoroutine(ChangeHoldBreatheStateStateAfterDelay());
    }

    private IEnumerator ChangeHoldBreatheStateStateAfterDelay()
    {
        var time = GameManager.instance.gameConfig.timeToBreakCoughState;
        yield return new WaitForSeconds(time);
        var targetState = player.stateMachine.prevState ?? player.holdBreatheState;

        stateMachine.ChangeState(targetState);
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.DecreaseOxygenOverTime();
        player.DecreaseCarbonDioxideByCough();
    }
}
