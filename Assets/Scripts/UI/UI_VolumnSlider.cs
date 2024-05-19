using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumnSlider : MonoBehaviour
{
    public Slider sliderVolume;
    public string parametr;
    
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier;

    public void SliderValue(float _value)
    {
        audioMixer.SetFloat(parametr, Mathf.Log10(_value)*multiplier);
    }
}
