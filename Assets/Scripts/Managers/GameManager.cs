using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BaseScreen[] screens;

    public Dictionary<string, BaseScreen> screenDict = new Dictionary<string, BaseScreen>();

    public GameConfig gameConfig;
    public Player player;

    private bool isPlaying = false;

    public float time;

    public int currentLevel = 1;

    #region Debug
    [SerializeField]
    private TextMeshProUGUI playerStateText;
    #endregion
    [SerializeField] private Transform spawnPoint;

    [SerializeField]
    private Smoke smoke;
    [SerializeField]
    private WaterBehavior water;

    [SerializeField]
    private FlushButton flushButton;

    [SerializeField]
    private Thermometer thermometer;

    [SerializeField]
    private Tube tube;

    #region Prefabs
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private GameObject waterPrefab;
    #endregion


    private void Awake()
    {
        instance = this;
        AddScreens();
    }

    private void Start()
    {
        screenDict[ScreenKeys.MENU_SCREEN]?.Open();
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
        player.Init();
        time = 0;
        isPlaying = true;

        PrepareLevel();
    }

    public void PrepareLevel()
    {
        smoke.gameObject.SetActive(false);
        water.gameObject.SetActive(false);
        flushButton.gameObject.SetActive(false);

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
        //
    }

    public void PrepareLevel2()
    {
        tube.gameObject.SetActive(true);
        tube.Init(() =>
        {
            thermometer.ReduceTemperature(10);
        });

        thermometer.gameObject.SetActive(true);
        thermometer.Init();
    }

    public void PrepareLevel3()
    {
        water.gameObject.SetActive(true);
        water.Init();
        flushButton.gameObject.SetActive(true);
        flushButton.Init();
    }

    public void PrepareLevel4()
    {
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

    public void HandleGameLose()
    {
        isPlaying = false;
        player.stateMachine.ChangeState(player.dieState);

        var resultScreen = screenDict[ScreenKeys.RESULT_SCREEN] as ResultScreen;
        resultScreen.UpdateScreen(false);
        screenDict[ScreenKeys.RESULT_SCREEN].Open();
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
        var waterObject = Instantiate(waterPrefab);
        water = waterObject.GetComponent<WaterBehavior>();
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
}
