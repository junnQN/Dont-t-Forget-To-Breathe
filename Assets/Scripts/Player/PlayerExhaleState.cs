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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        var oxygenAmount = -player.playerConfig.autoRate * Time.deltaTime;
        var carbonDioxideAmount = -player.playerConfig.exhaleRate * Time.deltaTime;

        player.ChangeCarbonDioxide(carbonDioxideAmount);
        player.ChangeOxygen(oxygenAmount);
        if (Input.GetKeyUp(KeyCode.O))
            player.stateMachine.ChangeState(player.holdBreatheState);

    }
}
