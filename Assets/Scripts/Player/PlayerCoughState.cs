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

        if (!player.isDisableInput && Input.GetKey(KeyCode.I))
            player.stateMachine.ChangeState(player.inhaleState);
        else if (!player.isDisableInput && Input.GetKey(KeyCode.O))
            player.stateMachine.ChangeState(player.exhaleState);
        else player.stateMachine.ChangeState(player.holdBreatheState);
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
