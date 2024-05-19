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

    public void HandleStartButton()
    {
        // UI_Game.SetActive(false);
        Close();
        GameManager.instance.StartDropCat();
    }
}
