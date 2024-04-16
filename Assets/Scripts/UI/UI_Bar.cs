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
    [SerializeField] private int numberOfHearts = 4;
    [SerializeField] private int health;

    private void Update()
    {
        UpdateBar(oxygenSlider, player.oxygen);
        UpdateBar(carbonDioxideSlider, player.carbonDioxide);
        UpdateHeart();
        if (player.oxygen =< 0f || player.carbonDioxide >= 100f)
        {
            health--;
            player.oxygen = 100f;
            player.carbonDioxide = 0f;
        }
    }

    private void UpdateBar(Slider bar, float currVal)
    {
        bar.value = currVal / 100;
    }

    private void UpdateHeart()
    {
        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numberOfHearts)
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
