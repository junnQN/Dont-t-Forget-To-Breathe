using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public bool isEnable = true;
    public Debugger debugger;
    public TMPro.TextMeshProUGUI text;

    private void Start()
    {
        UpdateText();
    }
    public void OnClick()
    {
        debugger.PauseGame(isEnable);
        isEnable = !isEnable;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = isEnable ? "Pause" : "Resume";
    }
}
