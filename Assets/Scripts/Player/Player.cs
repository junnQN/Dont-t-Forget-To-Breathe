using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [SerializeField] public float oxygen = 100f;
    [SerializeField] private float oxygenRate = 10f;
    [SerializeField] public float carbonDioxide;
    [SerializeField] private float carbonDioxideRate = 10f;
    [SerializeField] private GameObject bucket;

    //=======
    //[SerializeField] public float oxygen = 100f;
    //[SerializeField] public float carbonDioxide;

    [SerializeField] public int currentHealth = 9;
    [SerializeField] public int maxHealth = 9;

    //>>>>>>> origin/quan
    public int facingDir { get; private set; } = 1;
    private bool facingRight = false;
    public bool isPlayerTouching = false;
    public bool inAir = false;
    public bool isTouchFlushButton = false;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public CharacterStats stats { get; private set; }
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

    private bool isAffectedBySmoke;

    //>>>>>>> origin/quan
    #endregion

    public bool isDisableInput = false;

    Tween smokeAffectedTween;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        //holdBreatheState = new PlayerHoldBreatheState(this, stateMachine, "HoldBreathe");
        //<<<<<<< HEAD
        inhaleState = new PlayerInhaleState(this, stateMachine, "Inhale");
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
        stats = GetComponent<CharacterStats>();
    }

    public void Init()
    {
        isDisableInput = false;
        isAffectedBySmoke = false;
        oxygen = 100f;
        carbonDioxide = 0f;
        currentHealth = 9;
        stateMachine.ChangeState(idleState);
    }

    private void Update()
    {
        if (oxygen <= 0f || carbonDioxide >= 100f)
        {
            currentHealth = Mathf.Clamp(currentHealth - 1, 0, maxHealth);
            CheckGameOver();
            oxygen = 100f;
            carbonDioxide = 0f;
            return;
        }

        if (stateMachine.currentState != null)
            stateMachine.currentState.Update();

        if (isTouchFlushButton && !isDisableInput && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.SprintWater();
        }
    }

    public void CheckGameOver()
    {
        if (currentHealth <= 0)
        {
            GameManager.instance.HandleGameLose();
        }
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
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(gameConfig.inhaleRate * Time.deltaTime);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDisableInput && Input.GetKeyDown(KeyCode.E))
        {
            bucket.transform.Rotate(0, 0, 90);
        }
        //=======
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(gameConfig.inhaleRate * Time.deltaTime);
    }

    private void OnParticleCollision(GameObject other)
    {
        switch (other.tag)
        {
            case "Smoke":
                AffectedBySmoke();
                break;
            default:
                break;
        }

    }

    void AffectedBySmoke()
    {
        if (isAffectedBySmoke) return;

        isAffectedBySmoke = true;
        isDisableInput = true;
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(-gameConfig.amountAirBySmoke);
        ChangeCarbonDioxide(-gameConfig.amountAirBySmoke);


        var temp = DOTween.Sequence()
            .AppendCallback(() => gameObject.SetActive(false))
            .AppendInterval(0.1f)
            .AppendCallback(() => gameObject.SetActive(true))
            .AppendInterval(0.1f)
            .SetLoops(-1);

        smokeAffectedTween?.Kill();
        smokeAffectedTween = DOVirtual.DelayedCall(gameConfig.timeAffectedBySmoke, () =>
        {
            isDisableInput = false;

            DOVirtual.DelayedCall(gameConfig.smokeImmunityTime, () =>
            {
                temp.Kill();
                isAffectedBySmoke = false;
                gameObject.SetActive(true);
            });
        });

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
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Bowl":
                isPlayerTouching = true;
                break;
            case "FlushButton":
                isTouchFlushButton = true;
                break;
            default:
                Debug.Log(other.tag);
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bowl"))
        {
            isPlayerTouching = false;
        }
        else if (other.CompareTag("FlushButton"))
        {
            isTouchFlushButton = true;
        }
    }


}


