using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheButton : MonoBehaviour
{
    public bool isEnable = true;
    public Debugger debugger;
    public TMPro.TextMeshProUGUI text;

    private void Start()
    {
        if (GameManager.instance != null)
        {
            isEnable = !GameManager.instance.isDisableBreath;
        }

        UpdateText();
    }
    public void OnClick()
    {
        debugger.DisableBreath(isEnable);
        isEnable = !isEnable;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = isEnable ? "Disable Breath" : "Enable Breath";
    }
}
