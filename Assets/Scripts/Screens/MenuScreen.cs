using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : BaseScreen
{
    [SerializeField] private GameObject UI_Game;
    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleRestartButton()
    {
        AudioManager.instance.PlaySFX(8);
        UI_Game.SetActive(false);
        Hand.instance.canPlay = true;
        Close();
        //GameManager.instance.StartGame();
        //UI_Tutorials.SetActive(true);
        //TutorialSwitch.instance.isTutorial = true;
        //UI_game.SetActive(false);
    }
}
