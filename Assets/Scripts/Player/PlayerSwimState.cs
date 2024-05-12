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
        
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (Input.GetKey(KeyCode.Space)&&player.transform.position.y<2.54f)
        {
            Swim();
        }
        Sink();
    }
    
    private void Swim()
    {
        // Áp dụng lực đẩy lên nhân vật khi bơi
        rb.AddForce(Vector2.up * player.swimForce, ForceMode2D.Impulse);
    }

    private void Sink()
    {
        // Áp dụng lực chìm khi không bơi
        rb.AddForce(Vector2.down * player.sinkSpeed, ForceMode2D.Force);
    }
}
