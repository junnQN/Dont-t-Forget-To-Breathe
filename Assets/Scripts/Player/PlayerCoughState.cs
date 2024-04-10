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
        yield return new WaitForSeconds(player.playerConfig.timeToBreakCoughState);
        stateMachine.ChangeState(player.holdBreatheState);
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        var oxygenAmount = -player.playerConfig.autoRate * Time.deltaTime;
        var carbonDioxideAmount = player.playerConfig.autoRate * Time.deltaTime;

        player.ChangeOxygen(oxygenAmount);
        player.ChangeCarbonDioxide(carbonDioxideAmount);
    }
}
