using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwimState : PlayerState
{
    
    public PlayerSwimState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        
        if (Input.GetKey(KeyCode.Space))
        {
            player.isSwimming = true;
        }
        else{
            player.isSwimming = false;
        }
        if (player.isSwimming&&player.transform.position.y<2.47f)
        {
            Swim();
        }
    }
    
    public void Swim()
    {
        // Tạo lực nổi khi ấn Space
        rb.AddForce(Vector2.up * player.swimForce, ForceMode2D.Force);

        // Giới hạn tốc độ nếu nó vượt quá maxVelocity
        if (rb.velocity.magnitude > player.maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * player.maxVelocity;
        }

        if (!player.IsGroundDetected())
        {
            rb.AddForce(Vector2.right * xInput * player.swimHorizontalForce, ForceMode2D.Force);
            //rb.AddForce(-rb.velocity * player.swimDrag);
        }
    }
}
