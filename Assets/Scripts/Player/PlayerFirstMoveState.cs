using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstMoveState : PlayerState
{
    
    public PlayerFirstMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        
        if (player.shouldMove)
        {
            // Di chuyển vật thể sang trái
            player.transform.Translate(Vector3.left * 2f * Time.deltaTime);

            // Kiểm tra nếu đến vị trí dừng
            if (player.transform.position.x <= -7f)
            {
                
                player.shouldMove = false;
                player.Flip();
                player.ActiveUI();
                if (GameManager.instance.currentLevel == 1)
                { 
                    stateMachine.ChangeState(player.noneState);
                }

                if (GameManager.instance.currentLevel==2)
                {
                    stateMachine.ChangeState(player.idleState);
                    GameManager.instance.isPlaying = true;
                }
            }
        }
    }
}
