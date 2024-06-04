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

    [SerializeField] private GameObject backToMenuBtn;

    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleRestartButton()
    {
        if (GameManager.instance.currentLevel == 1)
        {
            Player.instance.ReturnDefaultPos();
            GameManager.instance.StartGame();
            Close();
        }

        if (GameManager.instance.currentLevel >= 2)
        {
            if (Player.instance.currentHealth < 9)
            {
                Player.instance.ReturnDefaultPos();
                Player.instance.ChangeStateFisrtLv();
                GameManager.instance.RestartGame();
                Close();
            }
            else
            {
                GameManager.instance.NextLevel();
                Close();
            }
        }
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
        backToMenuBtn.SetActive(false);
    }

    public void BackToMainMenu()
    {
        this.title.text = "You Lose!";
        backToMenuBtn.SetActive(true);
        replayButton.SetActive(false);
        nextLevelButton.SetActive(false);
    }
}
