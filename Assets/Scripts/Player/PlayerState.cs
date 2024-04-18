using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

//<<<<<<< HEAD
    protected float xInput;
    //private string animBoolName;
    
    //public PlayerState(Player _player,PlayerStateMachine _stateMachine, string _animBoolName)
//=======
    public string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
//>>>>>>> origin/quan
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
//<<<<<<< HEAD
        player.anim.SetBool(animBoolName,true);
        rb = player.rb;
//=======
        player.anim.SetBool(animBoolName, true);
//>>>>>>> origin/quan
    }

    public virtual void Update()
    {
//<<<<<<< HEAD
        xInput = Input.GetAxisRaw("Horizontal");
//=======
        // Debug.Log("I in " + animBoolName);
//>>>>>>> origin/quan
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
