using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameConfig gameConfig;
    public Player player;

    private bool isPlaying = false;

    public float time;

    public int currentLevel = 1;

    #region Debug
    [SerializeField]
    private TextMeshProUGUI playerStateText;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        player.Init();
        time = 0;
        isPlaying = true;
    }

    private void Update()
    {
        if (!isPlaying) return;

        time += Time.deltaTime;

        playerStateText.text = player.stateMachine.currentState.animBoolName ?? "None";

        CheckGameOver();
    }

    public int GetRemainingTime()
    {
        return (int)gameConfig.timeOfLevels[currentLevel - 1] - (int)time;
    }

    private void CheckGameOver()
    {
        if (time >= gameConfig.timeOfLevels[currentLevel - 1])
        {
            player.stateMachine.ChangeState(player.dieState);
            Debug.Log("Game Over");
        }
    }
}
