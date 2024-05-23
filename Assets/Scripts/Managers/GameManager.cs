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
    public bool isWater = false;

    #region Debug
    [SerializeField]
    private TextMeshProUGUI playerStateText;
    #endregion
    [SerializeField] private Transform spawnPoint;

    [SerializeField]
    public Smoke smoke;
    [SerializeField]
    private WaterBehavior water;

    [SerializeField] private FlushButton flushButton;
    [SerializeField] private ReleaseWaterButton releaseButton;

    [SerializeField]
    private Thermometer thermometer;

    public Tube tube;

    public FallGlass fallGlass;

    public Cold cold;

    public BaseItem box;

    public BombBehavior bomb;

    public IntroManager introManager;

    public GameObject UI_Game;

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
            if (currentLevel == 2 || currentLevel == 5)
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

        introManager.Init();
    }

    public void StartGame(bool isRestart = false)
    {
        if (isRestart)
        {
            PrepareLevel();
        }
        UI_Game.SetActive(true);
        player.gameObject.SetActive(true);
        player.Init(!isRestart);
        time = 0;
        isPlaying = true;

        if (currentLevel == 3)
        {
            player.ChangeSwimState();
        }
    }

    public void StartDropCat()
    {
        UI_Game.SetActive(false);
        PrepareLevel();
        player.gameObject.SetActive(false);
        introManager.gameObject.SetActive(true);
        introManager.PlayIntro();
    }

    public void StartTutorial()
    {
        screenDict[ScreenKeys.TUTORIAL_SCREEN].Open();
    }

    public void NextLevel()
    {
        player.Init(false);
        time = 0;
        isPlaying = false;

        PrepareLevel();
    }

    public void RestartGame()
    {
        StartGame(true);
    }

    public void PrepareLevel()
    {
        smoke.gameObject.SetActive(false);
        water.gameObject.SetActive(false);
        flushButton.gameObject.SetActive(false);
        cold.gameObject.SetActive(false);
        bomb.gameObject.SetActive(false);
        thermometer.Init();
        fallGlass.Init();
        box.Init();

        fallGlass.IgnoreCollision(false);
        fallGlass.gameObject.SetActive(true);
        AudioManager.instance.StopSFX(11);

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
            case 5:
                PrepareLevel5();
                break;
            default:
                break;
        }
    }

    public void PrepareLevel1()
    {

    }

    public void PrepareLevel2()
    {
        player.ReturnStartPos();
        player.gameObject.SetActive(false);
        // UI_Game.SetActive(false);
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

        AudioManager.instance.bgmIndex += 1;
        fallGlass.gameObject.SetActive(false);
        player.ReturnSwimPos();
        fallGlass.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        // UI_Game.SetActive(false);
        tube.gameObject.SetActive(true);
        box.gameObject.SetActive(false);
        water.gameObject.SetActive(true);
        water.Init();
        flushButton.gameObject.SetActive(true);
        flushButton.Init();
    }

    public void PrepareLevel4()
    {
        fallGlass.gameObject.SetActive(true);
        ResetPos.instance.ResetPosition();
        box.gameObject.SetActive(false);
        player.ReturnStartPos();
        player.gameObject.SetActive(false);
        // UI_Game.SetActive(false);
        smoke.gameObject.SetActive(true);
        smoke.Init();
    }

    public void PrepareLevel5()
    {
        bomb.Init();
        AudioManager.instance.bgmIndex += 1;
        releaseButton.gameObject.SetActive(true);
        releaseButton.Init();
        flushButton.gameObject.SetActive(true);
        flushButton.Init();
        tube.gameObject.SetActive(true);

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
            AudioManager.instance.PlaySFX(9);
            HandleGameWin();
        }
    }

    public void HandleGameWin()
    {
        AudioManager.instance.PlaySFX(14);
        player.stateMachine.ChangeState(player.noneState);
        isPlaying = false;
        screenDict[ScreenKeys.WIN_SCREEN].Open();
        currentLevel++;
    }

    public void CheckGameLose()
    {
        if (player.currentHealth == player.tmpHealth - 1)
        {
            player.tmpHealth -= 1;
            player.stateMachine.ChangeState(player.dieState);
            // HandleGameLose();
        }
    }
    public void HandleGameLose()
    {
        AudioManager.instance.PlaySFX(13);
        isPlaying = false;
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
        smoke.gameObject.transform.position = spawnPoint.position;
        smoke.gameObject.SetActive(true);

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
        AudioManager.instance.PlaySFX(20);
        isWater = false;
        water.SprintWater();
        flushButton.ChangeButtonState(true);
        player.ChangeStateFisrtLv();
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

    public void OpenCutScreen(bool isHulk)
    {
        var cutScreen = screenDict[ScreenKeys.CUT_SCREEN] as CutScreen;
        cutScreen.isHulk = isHulk;
        cutScreen.Open();
    }

    public void CloseScreen(string screenName)
    {
        screenDict[screenName].Close();
    }
}
