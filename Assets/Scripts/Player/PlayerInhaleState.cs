using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInhaleState : PlayerState
{
    public PlayerInhaleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StartCoroutine(ChangeHoldBreatheStateStateAfterDelay());
    }

    private IEnumerator ChangeHoldBreatheStateStateAfterDelay()
    {
        var time = GameManager.instance.gameConfig.maxTimeInhale;
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

        player.IncreaseOxygenByInhale();
        player.IncreaseCarbonDioxideOverTime();

        if (Input.GetKeyUp(KeyCode.I))
        {
            player.stateMachine.ChangeState(player.holdBreatheState);
        }
    }
}
