using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        var remainTime = GameManager.instance.GetRemainingTime();
        if (remainTime <= 0)
        {
            timerText.text = "0";
            return;
        }

        timerText.text = remainTime.ToString();
    }
}
