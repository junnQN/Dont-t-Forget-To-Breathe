using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BaseScreen[] screens;

    public Dictionary<string, BaseScreen> screenDict = new Dictionary<string, BaseScreen>();

    public GameConfig gameConfig;
    public Player player;

    public bool isPlaying = false;

    public float time;

    public int currentLevel = 1;

    #region Debug
    [SerializeField]
    private TextMeshProUGUI playerStateText;
    #endregion
    [SerializeField] private Transform spawnPoint;

    [SerializeField]
    public Smoke smoke;
    [SerializeField]
    private WaterBehavior water;

    [SerializeField]
    private FlushButton flushButton;

    [SerializeField]
    private Thermometer thermometer;

    public Tube tube;

    public FallGlass fallGlass;

    public Cold cold;

    #region Prefabs
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject waterPrefab;
    #endregion

    [SerializeField] private GameObject UI_Game;
    [SerializeField] private GameObject block;

    public bool isDisableBreath = false;

    private void Awake()
    {
        instance = this;
        AddScreens();
    }

    public void Start()
    {
        screenDict[ScreenKeys.MENU_SCREEN]?.Open();

        tube.onCollisionBox += () =>
        {
            if (currentLevel == 2)
            {
                Debug.Log("Tube Collision");
                cold.ExitCold(() =>
                {
                    Debug.Log("Exit Cold");
                    player.isCold = false;
                });
            }
            else if (currentLevel == 3)
            {
                // water.ChangeWaterHeight(0.1f);
            }
        };
    }

    public void StartGame()
    {
        player.Init();
        time = 0;
        isPlaying = true;
        PrepareLevel();
    }

    public void NextLevel()
    {
        player.Init2();
        time = 0;
        isPlaying = false;

        PrepareLevel();
    }

    public void RestartGame()
    {
        time = 0;
        isPlaying = true;
    }

    public void PrepareLevel()
    {
        smoke.gameObject.SetActive(false);
        water.gameObject.SetActive(false);
        flushButton.gameObject.SetActive(false);
        cold.gameObject.SetActive(false);

        thermometer.gameObject.SetActive(true);
        thermometer.Init();
        fallGlass.Init();

        switch (currentLevel)
        {
            case 1:
                PrepareLevel1();
                break;
            case 2:
                PrepareLevel2();
                break;
            case 3:
                PrepareLevel3();
                break;
            case 4:
                PrepareLevel4();
                break;
            default:
                break;
        }
    }

    public void PrepareLevel1()
    {
        // player.ChangeSwimState();
    }

    public void PrepareLevel2()
    {
        player.isCold = true;
        player.ReturnStartPos();
        player.gameObject.SetActive(false);
        UI_Game.SetActive(false);
        Hand.instance.canPlay = true;
        Hand.instance.moveDown = true;
        tube.gameObject.SetActive(true);
        tube.Init(() =>
        {
            cold.gameObject.SetActive(true);
            cold.Init(() =>
            {
                player.isCold = true;
            });
        });
    }

    public void PrepareLevel3()
    {
        player.ReturnStartPos();
        player.gameObject.SetActive(false);
        UI_Game.SetActive(false);
        Hand.instance.canPlay = true;
        Hand.instance.moveDown = true;
        tube.gameObject.SetActive(true);
        block.SetActive(false);
        water.gameObject.SetActive(true);
        water.Init();
        flushButton.gameObject.SetActive(true);
        flushButton.Init();
        //player.ChangeSwimState();
    }

    public void PrepareLevel4()
    {
        block.SetActive(false);
        player.ReturnStartPos();
        player.gameObject.SetActive(false);
        UI_Game.SetActive(false);
        Hand.instance.canPlay = true;
        Hand.instance.moveDown = true;
        smoke.gameObject.SetActive(true);
        smoke.Init();
    }

    public void AddScreens()
    {
        foreach (var screen in screens)
            screenDict.Add(screen.screenName, screen);
    }

    private void Update()
    {
        if (!isPlaying) return;

        time += Time.deltaTime;

        playerStateText.text = player.stateMachine.currentState.animBoolName ?? "None";

        CheckGameWin();
        CheckGameLose();
    }

    public int GetRemainingTime()
    {
        return (int)gameConfig.timeOfLevels[currentLevel - 1] - (int)time;
    }

    private void CheckGameWin()
    {
        if (isPlaying && time >= gameConfig.timeOfLevels[currentLevel - 1])
        {
            HandleGameWin();
        }
    }

    public void HandleGameWin()
    {
        player.stateMachine.ChangeState(player.noneState);
        isPlaying = false;
        currentLevel++;
        var resultScreen = screenDict[ScreenKeys.RESULT_SCREEN] as ResultScreen;
        resultScreen.UpdateScreen(true);
        screenDict[ScreenKeys.RESULT_SCREEN].Open();
    }

    public void CheckGameLose()
    {
        if (player.currentHealth == player.tmpHealth - 1)
        {
            player.tmpHealth -= 1;
            player.stateMachine.ChangeState(player.noneState);
            HandleGameLose();
        }
    }
    public void HandleGameLose()
    {
        isPlaying = false;
        player.stateMachine.ChangeState(player.dieState);
        var resultScreen = screenDict[ScreenKeys.RESULT_SCREEN] as ResultScreen;
        if (player.currentHealth == 0f)
        {
            resultScreen.BackToMainMenu();
            screenDict[ScreenKeys.RESULT_SCREEN].Open();
        }
        else
        {
            resultScreen.UpdateScreen(false);
            screenDict[ScreenKeys.RESULT_SCREEN].Open();
        }

    }

    public void SpawnSmoke()
    {
        if (smoke == null)
        {
            var smokeObject = Instantiate(smokePrefab, spawnPoint.position, Quaternion.identity);
            smoke = smokeObject.GetComponent<Smoke>();
        }
        else
        {
            smoke.gameObject.transform.position = spawnPoint.position;
            smoke.gameObject.SetActive(true);
        }

        smoke.Init();
        smoke.ShowSmoke();
    }

    public void SpawnWater()
    {
        water.gameObject.SetActive(true);
        water.Init();
    }

    public void SprintWater()
    {
        water.SprintWater();
        flushButton.ChangeButtonState(true);
    }


    public void HideSmoke()
    {
        smoke.ReduceSmoke();
    }

    public bool isHaveSmoke()
    {
        if (smoke == null)
        {
            return false;
        }
        return smoke.isPlaying;
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ResetScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
