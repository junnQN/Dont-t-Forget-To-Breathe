using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
//<<<<<<< HEAD
    [Header("Move info")] 
    public float moveSpeed = 12f;
    public float jumpForce;
    
    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    
    [SerializeField] public float oxygen=100f;
    [SerializeField] private float oxygenRate = 10f;
    [SerializeField] public float carbonDioxide;
    [SerializeField] private float carbonDioxideRate = 10f;
    [SerializeField] private GameObject bucket;
    
//=======
    //[SerializeField] public float oxygen = 100f;
    //[SerializeField] public float carbonDioxide;

//>>>>>>> origin/quan
    public int facingDir { get; private set; } = 1;
    private bool facingRight = false; 
    public bool isPlayerTouching = false;
    public bool inAir = false;
    
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerInhaleState inhaleState { get; private set; }
    public PlayerExhaleState exhaleState { get; private set; }
    public PlayerHoldBreatheState holdBreatheState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerEatState eatState { get; private set; }
//<<<<<<< HEAD
    
//=======
    public PlayerCoughState coughState { get; private set; }
    public PlayerNoneState noneState { get; private set; }
    public PlayerDieState dieState { get; private set; }

//>>>>>>> origin/quan
    #endregion
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        //holdBreatheState = new PlayerHoldBreatheState(this, stateMachine, "HoldBreathe");
//<<<<<<< HEAD
        inhaleState = new PlayerInhaleState(this,stateMachine,"Inhale");
        //exhaleState = new PlayerExhaleState(this,stateMachine,"Exhale");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        eatState = new PlayerEatState(this, stateMachine, "Eat");
//=======
        inhaleState = new PlayerInhaleState(this, stateMachine, "Inhale");
        exhaleState = new PlayerExhaleState(this, stateMachine, "Exhale");
        coughState = new PlayerCoughState(this, stateMachine, "Cough");
        noneState = new PlayerNoneState(this, stateMachine, "None");
        dieState = new PlayerDieState(this, stateMachine, "Die");
//>>>>>>> origin/quan
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
//<<<<<<< HEAD
        rb = GetComponent<Rigidbody2D>();
        //stateMachine.Initialize(holdBreatheState);
//=======
        stateMachine.Initialize(noneState);
    }

    public void Init()
    {
        oxygen = 100f;
        carbonDioxide = 0f;
        stateMachine.ChangeState(idleState);
//>>>>>>> origin/quan
    }

    private void Update()
    {
        
//<<<<<<< HEAD
        //stateMachine.currentState.Update();
//=======
        if (stateMachine.currentState != null)
            stateMachine.currentState.Update();
//>>>>>>> origin/quan
    }

    public void ChangeOxygen(float amount)
    {
        oxygen += amount;
        oxygen = Mathf.Clamp(oxygen, 0, 100);
    }

    public void ChangeCarbonDioxide(float amount)
    {
        carbonDioxide += amount;
        carbonDioxide = Mathf.Clamp(carbonDioxide, 0, 100);
    }

    public void DecreaseOxygenOverTime()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(-gameConfig.autoRate * Time.deltaTime);
    }

    public void IncreaseOxygenByInhale()
    {
//<<<<<<< HEAD
        carbonDioxide+= carbonDioxideRate * Time.deltaTime;
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bucket.transform.Rotate(0,0,90);
        }
//=======
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(gameConfig.inhaleRate * Time.deltaTime);
    }

    public void IncreaseOxygenBySmoke()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(gameConfig.amountAirBySmoke * Time.deltaTime);
    }

    public void IncreaseCarbonDioxideBySmoke()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(gameConfig.amountAirBySmoke * Time.deltaTime);
    }

    public void DecreaseCarbonDioxideByExhale()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(-gameConfig.exhaleRate * Time.deltaTime);
    }

    public void IncreaseCarbonDioxideOverTime()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(gameConfig.autoRate * Time.deltaTime);
    }

    public void DecreaseCarbonDioxideByCough()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(-gameConfig.amountCo_2Cough * Time.deltaTime);
//>>>>>>> origin/quan
    }

    public bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+wallCheckDistance,wallCheck.position.y));
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x<0&&facingRight)
        {
            Flip();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bowl"))
        {
            isPlayerTouching = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bowl"))
        {
            isPlayerTouching = false;
        }
    }
    
    
}


