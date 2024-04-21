using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInhaleState : PlayerGroundState
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
        stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

//<<<<<<< HEAD
        
        //player.IncreaseOxygen();
        //player.IncreaseCarbonDioxide();
//=======
        if (GameManager.instance.isHaveSmoke())
        {
            if (HandleInhaleSmoke()) return;

            player.IncreaseCarbonDioxideBySmoke();
            player.IncreaseOxygenBySmoke();
        }
        else
        {
            player.IncreaseOxygenByInhale();
            player.IncreaseCarbonDioxideOverTime();
        }


//>>>>>>> origin/quan
        if (Input.GetKeyUp(KeyCode.I))
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }

    bool HandleInhaleSmoke()
    {
        double probability = GameManager.instance.gameConfig.coughRate;

        if (ShouldCallFunction(probability))
        {
            player.stateMachine.ChangeState(player.coughState);
            return true;
        }
        return false;
    }

    bool ShouldCallFunction(double probability)
    {
        double randomValue = Random.value;

        return randomValue < probability;
    }
}
