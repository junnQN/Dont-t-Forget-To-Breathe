using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : BaseScreen
{
    [SerializeField] private GameObject UI_Tutorials;
    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleRestartButton()
    {
        //GameManager.instance.StartGame();
        //Hand.instance.canPlay = true;
        UI_Tutorials.SetActive(true);
        TutorialSwitch.instance.isTutorial = true;
        Close();
        //UI_game.SetActive(false);
    }
}
