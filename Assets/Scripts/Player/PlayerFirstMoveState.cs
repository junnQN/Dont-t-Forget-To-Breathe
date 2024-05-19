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
        if (GameManager.instance.currentLevel != 3)
        {
            //AudioManager.instance.PlaySFX(1);
            AudioManager.instance.PlaySFX(5);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        if(GameManager.instance.currentLevel!=3)
        {
            //AudioManager.instance.PlaySFX(1);
            AudioManager.instance.StopSFX(5);
        }
    }

    public override void Update()
    {
        base.Update();

        if (player.shouldMove)
        {
            // Di chuyển vật thể sang trái
            player.transform.Translate(Vector3.left * 2f * Time.deltaTime);

            if (GameManager.instance.currentLevel == 3)
            {
                player.shouldMove = false;
                //player.Flip();
                player.ActiveUI();
                stateMachine.ChangeState(player.swimState);
                GameManager.instance.isPlaying = true;
            }
            
            // Kiểm tra nếu đến vị trí dừng
            else if (player.transform.position.x <= -7f)
            {

                player.shouldMove = false;
                GameManager.instance.fallGlass.IgnoreCollision(false);
                player.Flip();
                player.ActiveUI();
                if (GameManager.instance.currentLevel == 1)
                {
                    stateMachine.ChangeState(player.noneState);
                }

                if (GameManager.instance.currentLevel == 2 || GameManager.instance.currentLevel > 3)
                {
                    stateMachine.ChangeState(player.idleState);
                    GameManager.instance.isPlaying = true;
                }
                
            }
        }
    }
}
