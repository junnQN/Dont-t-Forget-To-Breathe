using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExhaleState : PlayerState
{
    public PlayerExhaleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StartCoroutine(ChangeHoldBreatheStateStateAfterDelay());
    }

    private IEnumerator ChangeHoldBreatheStateStateAfterDelay()
    {
        var time = GameManager.instance.gameConfig.maxTimeExhale;
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState(player.holdBreatheState);
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
            player.stateMachine.ChangeState(player.holdBreatheState);

    }
}
