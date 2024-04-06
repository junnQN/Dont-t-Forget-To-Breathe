using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smokeParticle;

    private float rateOverTime = 15;

    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        var emission = smokeParticle.emission;
        emission.rateOverTime = rateOverTime;
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
