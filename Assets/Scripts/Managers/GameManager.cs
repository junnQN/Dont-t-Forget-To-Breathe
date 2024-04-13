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
    [SerializeField] private Transform spawnPoint;

    private Smoke smoke;

    #region Prefabs
    [SerializeField] private GameObject smokePrefab;
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
            player.stateMachine.ChangeState(player.noneState);
            Debug.Log("Game Over");
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
