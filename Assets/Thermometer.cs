using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer : MonoBehaviour
{
    public float currentTemperature;
    public TMPro.TextMeshProUGUI temperatureText;
    public Image progressBar;

    private Tween tween;
    public float timeToChangeTemperature = 2;

    public void Init()
    {
        temperatureText.text = $"{currentTemperature}°C";
        progressBar.fillAmount = currentTemperature / 100;
    }

    public void ReduceTemperature(float targetTemperature)
    {
        tween?.Kill();
        tween = DOVirtual.Float(currentTemperature, targetTemperature, timeToChangeTemperature, (v) =>
        {
            currentTemperature = Mathf.RoundToInt(v);
            temperatureText.text = $"{currentTemperature}°C";
            progressBar.fillAmount = currentTemperature / 100;
        });

    }
}
