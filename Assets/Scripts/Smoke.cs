using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smokeParticle;

    [SerializeField]
    private float rateOverTime = 50;

    [SerializeField]
    private bool isSwinging = false;

    [SerializeField]
    private float swingTime = 3f;

    [SerializeField]
    private float swingAngle = 40f;

    [SerializeField]
    private float targetAngleShape = 70f;

    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        var emission = smokeParticle.emission;
        emission.rateOverTime = rateOverTime;

        if (isSwinging)
        {
            SetSwinging();
        }
        else
        {
            SetShapeAngle();
        }
    }

    private void SetSwinging()
    {
        DOVirtual.Float(-1, 1, swingTime, (v) =>
        {
            var shape = smokeParticle.shape;
            var rotation = shape.rotation;
            rotation.y = swingAngle * v;
            shape.rotation = rotation;
        }).SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }

    private void SetShapeAngle()
    {
        DOVirtual.Float(30, targetAngleShape, 2f, (v) =>
        {
            var shape = smokeParticle.shape;
            shape.angle = v;
        });
    }

    public void ReduceSmoke()
    {
        StartCoroutine(ReduceSmokeOverTime());
    }

    private IEnumerator ReduceSmokeOverTime(float duration = 1f)
    {
        float elapsedTime = 0f;
        float startRate = smokeParticle.emission.rateOverTime.constant;
        float endRate = 0f;

        var emission = smokeParticle.emission;
        while (elapsedTime < duration)
        {
            float rate = Mathf.Lerp(startRate, endRate, elapsedTime / duration);
            emission.rateOverTime = rate;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        emission.rateOverTime = endRate;

        HideSmoke();
    }

    public void ShowSmoke()
    {
        isPlaying = true;
        smokeParticle.Play();
    }

    public void HideSmoke()
    {
        isPlaying = false;
        smokeParticle.Stop();
    }
}
