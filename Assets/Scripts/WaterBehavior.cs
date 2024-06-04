using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public float waterMaxHeight = 0.5f;
    public float waterCurrentHeight = 0.5f;

    public float startY = -3.48f;
    public float endY = 2.5f;

    public GameObject waterObject;
    Tween sprintTween;
    private Tween spawnTween;

    public void Init()
    {
        GameManager.instance.tube.ShowWaterFall();
        var gameConfig = GameManager.instance.gameConfig;
        waterMaxHeight = gameConfig.waterMaxHeight;
        SpawnWater();
    }

    public void SetWaterHeight(float height)
    {
        Camera mainCamera = Camera.main;

        waterCurrentHeight = height;
        waterObject.transform.localPosition = new Vector3(waterObject.transform.localPosition.x, height, waterObject.transform.localPosition.z);
    }

    public void ChangeWaterHeight(float amount)
    {
        waterCurrentHeight += amount;
        if (waterCurrentHeight > waterMaxHeight)
        {
            waterCurrentHeight = waterMaxHeight;
        }
        SetWaterHeight(waterCurrentHeight);
    }

    public void SprintWater()
    {
        sprintTween?.Kill();
        var time = GameManager.instance.gameConfig.sprintTime;
        sprintTween = DOVirtual.Float(waterCurrentHeight, 0, time, (v) =>
        {
            SetWaterHeight(v);
        });
    }

    public void SpawnWater()
    {
        spawnTween?.Kill();
        var time = GameManager.instance.gameConfig.spawnTime;
        spawnTween = DOVirtual.Float(startY, waterMaxHeight, time, (v) =>
        {
            SetWaterHeight(v);
        });
    }
}
