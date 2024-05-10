using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public GameObject gameConfigDebugger;

    public void ShowGameConfigDebugger(bool isShow)
    {
        gameConfigDebugger.SetActive(isShow);
    }

    public void PauseGame(bool isPause)
    {
        Time.timeScale = isPause ? 0 : 1;
    }

    public void DisableBreath(bool isDisable)
    {
        //
    }
}
