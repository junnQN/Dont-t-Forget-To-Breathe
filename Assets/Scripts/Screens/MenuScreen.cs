using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : BaseScreen
{
    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleRestartButton()
    {
        GameManager.instance.StartGame();
        Close();
    }
}
