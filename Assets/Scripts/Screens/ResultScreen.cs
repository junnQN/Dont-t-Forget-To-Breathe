using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreen : BaseScreen
{
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private GameObject replayButton;

    [SerializeField]
    private GameObject nextLevelButton;

    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleRestartButton()
    {
        GameManager.instance.StartGame();
        Close();
    }

    public void HandleNextButton()
    {
        GameManager.instance.NextLevel();
        Close();
    }

    public void UpdateScreen(bool isWin)
    {
        string title = isWin ? "You Win!" : "You Lose!";
        this.title.text = title;
        replayButton.SetActive(!isWin);
        nextLevelButton.SetActive(isWin);
    }
}
