using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour
{
    public static UI_Bar instance;
    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else 
            instance = this;
    }
    
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private Slider carbonDioxideSlider;
    [SerializeField] private Player player;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

//<<<<<<< HEAD
    [SerializeField] private int numberOfHearts;
    [SerializeField] private int health;
    [SerializeField] public int life;

//=====
//>>>>>>> origin/quan

    private void Update()
    {
        UpdateBar(oxygenSlider, player.oxygen);
        UpdateBar(carbonDioxideSlider, player.carbonDioxide);
        UpdateHeart();
        UpdateHealth();
    }

    private void UpdateBar(Slider bar, float currVal)
    {
        bar.value = currVal / 100;
    }

    private void UpdateHeart()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i <= player.currentHealth - 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i <= player.maxHealth - 1)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void UpdateHealth()
    {
        if (player.oxygen <= 0 || player.carbonDioxide >= 100f)
        {
            life--;
        }
    }

}
