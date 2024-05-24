using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public FireBehavior fire;

    public TMPro.TextMeshPro text;

    public Animator anim;

    public float time;

    private bool isPlaying = false;

    private bool isShowItem = false;

    public SupperItem item;

    [Button]
    public void Init()
    {
        anim.SetBool("Explode", false);
        // gameObject.SetActive(true);
        fire.Init();
        time = 0;
        isPlaying = false;
        item.Init();
        text.alpha = 1;
        isShowItem = false;
    }

    public void Play()
    {
        time = 0;
        isPlaying = true;
    }

    void Update()
    {
        if (isPlaying == false || GameManager.instance.isPlaying == false)
        {
            return;
        }

        time += Time.deltaTime;

        var timeToExplode = GameManager.instance.gameConfig.timeToExplode;
        var remainTime = (int)timeToExplode - (int)time;
        if (remainTime <= 0)
        {
            text.text = "0";
            isPlaying = false;
            anim.SetBool("Explode", true);
            text.alpha = 0;
            //! Dont sort this line
            isShowItem = !fire.gameObject.activeSelf;
            fire.gameObject.SetActive(false);

            CheckBombExplode();

            return;
        }

        text.text = remainTime.ToString();
    }

    public void Explode()
    {
        isPlaying = false;
        if (isShowItem)
        {
            item.Show();
        }
    }

    private void CheckBombExplode()
    {
        if (!isShowItem)
        {
            GameManager.instance.OpenCutScreen(false);
        }
    }
}
