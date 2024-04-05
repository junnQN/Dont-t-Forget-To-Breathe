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

    private void Update()
    {
        UpdateBar(oxygenSlider,player.oxygen);
        UpdateBar(carbonDioxideSlider,player.carbonDioxide);
    }

    private void UpdateBar(Slider bar,float currVal)
    {
        bar.value = currVal / 100;
    }
}
