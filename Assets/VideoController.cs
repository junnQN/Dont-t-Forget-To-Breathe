using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip explodeClip;
    public VideoClip hulkClip;

    Action onReachEnd;
    private void Start()
    {
        videoPlayer.loopPointReached += (VideoPlayer vp) =>
        {
            GameManager.instance.CloseScreen(ScreenKeys.CUT_SCREEN);
            onReachEnd?.Invoke();
        };
    }

    public void PlayExplodeClip()
    {
        videoPlayer.clip = explodeClip;
        videoPlayer.Play();
        onReachEnd = () =>
        {
            GameManager.instance.HandleGameLose();
        };
    }

    public void PlayHulkClip()
    {
        videoPlayer.clip = hulkClip;
        videoPlayer.Play();
        onReachEnd = () =>
        {
            GameManager.instance.HandleGameWin();
        };
    }
}
