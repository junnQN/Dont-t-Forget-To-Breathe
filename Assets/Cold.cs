using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cold : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    Tween tween;

    public bool isCold = false;

    public float timeToChangeTemperature = 2;
    public void Init(Action onComplete)
    {
        isCold = true;
        tween?.Kill();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        tween = spriteRenderer.DOFade(1f, timeToChangeTemperature).OnComplete(() =>
        {
            onComplete();
        });
    }

    public void ExitCold(Action onComplete)
    {
        if (!isCold) return;
        tween?.Kill();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        spriteRenderer.DOFade(0f, timeToChangeTemperature).OnComplete(() =>
        {
            isCold = false;
            onComplete();
        });
    }
}
