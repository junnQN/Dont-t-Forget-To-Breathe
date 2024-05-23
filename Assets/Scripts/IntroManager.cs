using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroManager : MonoBehaviour
{
    public PlayableDirector intro;

    public GameObject cat;

    public PlayableAsset introNormal;
    public PlayableAsset introLevel3;

    public PlayableAsset introLevel5;

    public void Init()
    {
        intro.stopped += (director) =>
        {
            var player = GameManager.instance.player;
            player.transform.position = cat.transform.position;
            if (cat.transform.rotation.y > 0)
            {
                player.Flip();
            }
            GameManager.instance.StartTutorial();
            gameObject.SetActive(false);

            if (GameManager.instance.currentLevel == 5)
            {
                GameManager.instance.bomb.gameObject.SetActive(true);
            }
        };
    }

    public void PlayIntro()
    {
        if (GameManager.instance.currentLevel == 3)
        {
            intro.playableAsset = introLevel3;
        }
        else if (GameManager.instance.currentLevel == 5)
        {
            intro.playableAsset = introLevel5;
        }
        else
        {
            intro.playableAsset = introNormal;
        }

        intro.Stop();
        intro.Play();
    }
}
