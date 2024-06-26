using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class Player : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public static Player instance;

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

    [Header("Swim info")]
    [SerializeField] private GameObject water;
    public float swimForce = 5f; // Lực nổi khi ấn Space
    public float swimHorizontalForce = 3f; // Lực di chuyển sang trái và phải khi bơi
    public float gravity = 1f; // Gravitational pull
    public float maxVelocity = 5f; // Tốc độ tối đa của player
    public bool isSwimming = false;
    public float swimDrag = 2f;
    public float sinkSpeed = 2f; // Lực cản nước

    public int facingDir { get; private set; } = 1;
    [HideInInspector] public bool facingRight = false;
    public bool isPlayerTouching = false;
    public bool isTouchItem = false;
    public bool inAir = false;

    public bool shouldMove = false;

    //=======
    public int currentHealth = 9;
    public int tmpHealth = 9;
    public int maxHealth = 9;
    public float inhaleTime = 3f;
    public float exhaleTime = 3f;
    private float decreaseRate = 1f;
    public float discountRatePerSecond = 1f; // Tỷ lệ giảm giá mỗi giây

    private float currentTime;
    private float currentValue;
    public float timer = 5;

    [Header("UI")]
    public GameObject UI_Game;
    public GameObject UI_Tutorials;

    [Header("Cold level")]
    public bool isCold = false;

    [Header("Level 5")]
    public bool isTouchFlushButton = false;
    public bool isTouchReleaseWaterButton = false;
    public bool isPressedButton = false;
    public Tube tube;
    public Cold cold;

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

    public PlayerFirstMoveState firstState { get; private set; }
    public PlayerSwimState swimState { get; private set; }

    public PlayerCoughState coughState { get; private set; }
    public PlayerNoneState noneState { get; private set; }
    public PlayerDieState dieState { get; private set; }

    private bool isAffectedBySmoke;

    //>>>>>>> origin/quan
    #endregion

    public bool isDisableInput = false;

    Tween smokeAffectedTween;

    public bool isHulk = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");

        inhaleState = new PlayerInhaleState(this, stateMachine, "Inhale");
        //exhaleState = new PlayerExhaleState(this,stateMachine,"Exhale");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        eatState = new PlayerEatState(this, stateMachine, "Eat");

        firstState = new PlayerFirstMoveState(this, stateMachine, "Move");
        swimState = new PlayerSwimState(this, stateMachine, "Jump");

        inhaleState = new PlayerInhaleState(this, stateMachine, "Inhale");
        exhaleState = new PlayerExhaleState(this, stateMachine, "Exhale");
        coughState = new PlayerCoughState(this, stateMachine, "Cough");
        noneState = new PlayerNoneState(this, stateMachine, "None");
        dieState = new PlayerDieState(this, stateMachine, "Die");

        currentTime = Time.time;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(moveState);
        StartCoroutine(DecreaseOverTime(inhaleTime));
    }

    private void FixedUpdate()
    {
        if (!isSwimming)
        {
            ApplyGravity();
        }
    }

    public void Init(bool isRestHealth = false)
    {
        oxygen = 100f;
        carbonDioxide = 0f;

        if (isRestHealth)
        {
            currentHealth = maxHealth;
            tmpHealth = maxHealth;
            currentHealth = tmpHealth;
        }

        isCold = false;
        stateMachine.ChangeState(idleState);
        isTouchItem = false;
        isHulk = false;
        isPressedButton = false;
        isTouchFlushButton = false;
        isTouchReleaseWaterButton = false;
    }


    private void Update()
    {
        stateMachine.currentState.Update();

        if (oxygen <= 0f || carbonDioxide >= 100f)
        {

            currentHealth = Mathf.Clamp(currentHealth - 1, 0, maxHealth);
            // CheckGameOver();
            oxygen = 100f;
            carbonDioxide = 0f;
            return;
        }

        if (stateMachine.currentState != null)
            stateMachine.currentState.Update();

        if (GameManager.instance.isWater && isTouchFlushButton && !isDisableInput && Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.instance.PlaySFX(16);
            GameManager.instance.SprintWater();
            if (GameManager.instance.currentLevel == 5)
            {
                tube.Init(() =>
                {
                    cold.gameObject.SetActive(true);
                    cold.Init(() =>
                    {
                        isCold = true;
                    });
                });
            }
        }

        if (isTouchReleaseWaterButton && !isDisableInput && !isPressedButton && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.SpawnWater();
            ChangeSwimState();
            ReleaseWaterButton.instance.ChangeButtonState(true);
            isPressedButton = true;


            //ReleaseWaterButton.instance.ChangeTag();
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            AudioManager.instance.StopSFX(19);
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            AudioManager.instance.StopSFX(18);
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
        if (GameManager.instance.isDisableBreath) return;
        oxygen += amount;
        oxygen = Mathf.Clamp(oxygen, 0, 100);
    }

    public void ChangeCarbonDioxide(float amount)
    {
        if (GameManager.instance.isDisableBreath) return;
        carbonDioxide += amount;
        carbonDioxide = Mathf.Clamp(carbonDioxide, 0, 100);
    }

    public void DecreaseOxygenOverTime()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(-gameConfig.autoRate * Time.deltaTime);
    }

    public void IncreaseAirByInhale()
    {
        AudioManager.instance.PlaySFX(18);
        AudioManager.instance.StopSFX(19);
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(gameConfig.inhaleRate * Time.deltaTime);
    }

    public void IncreaseOxygenByCold()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(gameConfig.inhaleRate * 0.5f * Time.deltaTime);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
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
        if (!GameManager.instance.isPlaying) return;
        if (isAffectedBySmoke) return;
        anim.SetBool("Cough", true);
        AudioManager.instance.PlaySFX(3);


        isAffectedBySmoke = true;
        isDisableInput = true;
        var gameConfig = GameManager.instance.gameConfig;
        ChangeOxygen(-gameConfig.amountAirBySmoke);
        ChangeCarbonDioxide(-gameConfig.amountAirBySmoke);


        var temp = DOTween.Sequence()
            .Append(spriteRenderer.DOFade(0f, 0.1f))
            .Append(spriteRenderer.DOFade(1f, 0.1f))
            .SetLoops(-1);

        smokeAffectedTween?.Kill();
        smokeAffectedTween = DOVirtual.DelayedCall(gameConfig.timeAffectedBySmoke, () =>
        {
            isDisableInput = false;
            anim.SetBool("Cough", false);
            AudioManager.instance.StopSFX(3);


            DOVirtual.DelayedCall(gameConfig.smokeImmunityTime, () =>
            {
                temp.Kill();
                isAffectedBySmoke = false;
                spriteRenderer.color = Color.white;
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

    public void DecreaseAirByExhale()
    {
        AudioManager.instance.PlaySFX(19);
        AudioManager.instance.StopSFX(18);
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(-gameConfig.exhaleRate * Time.deltaTime);
    }

    public void IncreaseCarbonDioxideOverTime()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(gameConfig.autoRate * Time.deltaTime);
    }

    public void DecreaseCarbonDioxideByCold()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(-gameConfig.exhaleRate * 0.5f * Time.deltaTime);
    }

    public void DecreaseCarbonDioxideByCough()
    {
        var gameConfig = GameManager.instance.gameConfig;
        ChangeCarbonDioxide(-gameConfig.amountCo_2Cough * Time.deltaTime);
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
            case "WaterGlass":
                isPlayerTouching = true;
                break;
            case "SupperItem":
                isTouchItem = true;
                break;
            case "ReleaseWaterButton":
                isTouchReleaseWaterButton = true;

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
        else
         if (other.CompareTag("FlushButton"))
        {
            isTouchFlushButton = false;
        }
        else if (other.CompareTag("SupperItem"))
        {
            isTouchItem = false;
        }
        else if (other.CompareTag("ReleaseWaterButton"))
        {
            isTouchReleaseWaterButton = false;
        }
    }


    public void ActiveUI()
    {
        UI_Game.SetActive(true);
    }



    private void ApplyGravity()
    {
        // Áp dụng trọng lực
        rb.AddForce(Vector2.down * gravity, ForceMode2D.Force);
    }

    public void ChangeSwimState()
    {
        stateMachine.ChangeState(swimState);
        //water.SetActive(true);
    }

    public void ReturnStartPos()
    {
        if (facingRight)
        {
            Flip();
        }
        transform.position = new Vector3(0.23f, -2.156f, transform.position.z);
        shouldMove = true;
    }

    public void ReturnSwimPos()
    {
        if (facingRight)
        {
            Flip();
        }
        transform.position = new Vector3(0.23f, 2.4f, transform.position.z);
        shouldMove = true;
    }

    IEnumerator DecreaseOverTime(float breathTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Giảm mỗi giây

            // Giảm giá trị
            breathTime -= decreaseRate;

            // Kiểm tra nếu giá trị đã giảm hết
            if (breathTime <= 0)
            {
                breathTime = 0; // Đảm bảo giá trị không âm
                break;
            }

        }
    }

    public void DecreaseInhaleTime()
    {

        StartCoroutine(DecreaseOverTime(inhaleTime));
    }

    public void DecreaseExhaleTime()
    {

        StartCoroutine(DecreaseOverTime(exhaleTime));
    }

    public void DecreaseTime(float currentValue)
    {
        float elapsedTime = Time.time - currentTime; // Thời gian đã trôi qua kể từ lần cập nhật trước
        currentTime = Time.time; // Cập nhật thời gian hiện tại

        // Giảm giá trị dựa trên thời gian trôi qua
        currentValue -= elapsedTime * discountRatePerSecond;

        // Kiểm tra nếu giá trị đã giảm xuống dưới 0
        if (currentValue < 0f)
        {
            currentValue = 0f;
        }

        // In ra giá trị hiện tại sau khi giảm giá
        Debug.Log("Current value: " + currentValue);
    }

    public void ChangeStateFisrtLv()
    {
        stateMachine.ChangeState(idleState);
    }


    public void ReturnDefaultPos()
    {
        transform.position = new Vector2(-6.64f, -2.371f);
        if (!facingRight)
        {
            Flip();
        }
    }

    public void ReturnSmokePos()
    {
        transform.position = new Vector2(-8f, transform.position.y);
        if (!facingRight)
        {
            Flip();
        }
    }
    
    public void ChangeNoneState()
    {
        stateMachine.ChangeState(noneState);
    }
}


