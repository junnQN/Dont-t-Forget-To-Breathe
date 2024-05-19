using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public Transform start;
    public Transform end;

    public GameObject waterObject;
    Tween sprintTween;
    private Tween spawnTween;

    public void Init()
    {
        AudioManager.instance.PlaySFX(10);
        GameManager.instance.tube.ShowWaterFall();
        var gameConfig = GameManager.instance.gameConfig;
        SetWaterHeight(start.localPosition.y);
        SpawnWater();
    }

    public void SetWaterHeight(float height)
    {
        waterObject.transform.localPosition = new Vector3(waterObject.transform.localPosition.x, height, waterObject.transform.localPosition.z);
    }

    public void ChangeWaterHeight(float amount)
    {
        SetWaterHeight(amount);
    }

    public void SprintWater()
    {
        sprintTween?.Kill();
        var time = GameManager.instance.gameConfig.sprintTime;
        sprintTween = DOVirtual.Float(waterObject.transform.localPosition.y, start.localPosition.y, time, (v) =>
        {
            SetWaterHeight(v);
        });
    }

    public void SpawnWater()
    {
        spawnTween?.Kill();
        var time = GameManager.instance.gameConfig.spawnTime;
        spawnTween = DOVirtual.Float(start.localPosition.y, end.localPosition.y, time, (v) =>
        {
            SetWaterHeight(v);
        });
    }
}
