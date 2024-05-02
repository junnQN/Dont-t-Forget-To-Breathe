using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public float waterMaxHeight = 0.5f;
    public float waterCurrentHeight = 0.5f;

    public GameObject waterObject;
    Tween sprintTween;
    public void Init()
    {
        var gameConfig = GameManager.instance.gameConfig;
        waterMaxHeight = gameConfig.waterMaxHeight;
        SetWaterHeight(waterMaxHeight);
    }

    public void SetWaterHeight(float height)
    {
        Camera mainCamera = Camera.main;

        waterCurrentHeight = height;
        waterObject.transform.localScale = new Vector3(waterObject.transform.localScale.x, waterCurrentHeight, waterObject.transform.localScale.z);
        waterObject.transform.localPosition = new Vector3(waterObject.transform.localPosition.x, -mainCamera.orthographicSize + waterCurrentHeight / 2, waterObject.transform.localPosition.z);
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
        sprintTween = DOVirtual.Float(waterCurrentHeight, 0, time, (v) => SetWaterHeight(v));
    }
}
