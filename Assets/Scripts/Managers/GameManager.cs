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

    private Smoke smoke;

    #region Prefabs
    [SerializeField] private GameObject smokePrefab;
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
    }

    public void NextLevel()
    {
        player.Init();
        time = 0;
        isPlaying = true;
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
        if (time >= gameConfig.timeOfLevels[currentLevel - 1])
        {
            HandleGameWin();
        }
    }

    public void HandleGameWin()
    {
        player.stateMachine.ChangeState(player.noneState);

        currentLevel++;
        var resultScreen = screenDict[ScreenKeys.RESULT_SCREEN] as ResultScreen;
        resultScreen.UpdateScreen(true);
        screenDict[ScreenKeys.RESULT_SCREEN].Open();
    }

    public void HandleGameLose()
    {
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
