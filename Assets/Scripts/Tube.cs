using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private float startX;
    [SerializeField] private float endX;

    [SerializeField] private float speed = 1;

    Tween moveTween;

    [SerializeField] private GameObject waterFall;

    public void Init(Action onComplete)
    {
        moveTween?.Kill();
        transform.localPosition = new Vector3(startX, transform.localPosition.y, transform.localPosition.z);
        moveTween = transform.DOLocalMoveX(endX, speed).OnComplete(() => onComplete());
    }

    public void ShowWaterFall()
    {
        waterFall.SetActive(true);
    }

    public void HideWaterFall()
    {
        waterFall.SetActive(false);
    }
}
