using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] private Slider carbonDioxideSlider;
    [SerializeField] private Player player;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    private void Update()
    {
        UpdateBar(oxygenSlider, player.oxygen);
        UpdateBar(carbonDioxideSlider, player.carbonDioxide);
        UpdateHeart();
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

}
