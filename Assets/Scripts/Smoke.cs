using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smokeParticle;

    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {

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
