using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDebugger : MonoBehaviour
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
        debugger.ShowGameConfigDebugger(isEnable);
        isEnable = !isEnable;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = isEnable ? "Show Config" : "Hide Config";
    }
}
