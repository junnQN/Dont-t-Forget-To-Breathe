using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoneState : PlayerState
{
    public PlayerNoneState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.timer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // if (stateTimer < 1 &&stateTimer>0 && GameManager.instance.currentLevel==1)
        // {
        //     GameManager.instance.isPlaying = true;
        //     player.UI_Tutorials.SetActive(true);
        //     TutorialSwitch.instance.isTutorial = true;
        // }
    }
}
