using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroManager : MonoBehaviour
{
    public PlayableDirector intro;

    private void Start()
    {
        intro = GetComponent<PlayableDirector>();
        PlayIntro(() =>
        {
            Debug.Log("Intro Complete");
        });
    }

    public void PlayIntro(Action OnIntroComplete)
    {
        intro.Play();
        intro.stopped += (director) =>
        {
            OnIntroComplete();
        };
    }
}
