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

        if (!player.isDisableInput && Input.GetKey(KeyCode.I) && player.transform.position.y <= 0.09f)
        {
            player.oxygen -= 100;
        }
        else if (!player.isDisableInput && Input.GetKey(KeyCode.I) && player.transform.position.y > 0.09f)
        {
            player.DecreaseTime(player.inhaleTime);
            player.IncreaseAirByInhale();
            player.IncreaseCarbonDioxideOverTime();
        }

        else if (!player.isDisableInput && Input.GetKey(KeyCode.O))
        {
            player.DecreaseExhaleTime();
            player.DecreaseAirByExhale();
            player.DecreaseOxygenOverTime();
        }
        else
        {
            player.DecreaseOxygenOverTime();
            player.IncreaseCarbonDioxideOverTime();
        }

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (Input.GetKey(KeyCode.Space) && player.transform.position.y < 2.54f)
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
