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

    public override void Open()
    {
        base.Open();
        AudioManager.instance.PlaySFX(9);
        AudioManager.instance.playBGM = false;
    }

    public override void Close()
    {
        base.Close();
        AudioManager.instance.StopSFX(9);
        AudioManager.instance.playBGM = true;
    }

    public void HandleRestartButton()
    {
        AudioManager.instance.playBGM = true;
        AudioManager.instance.StopSFX(9);
        AudioManager.instance.PlaySFX(8);
        if (GameManager.instance.currentLevel == 1)
        {
            Player.instance.ReturnDefaultPos();
            GameManager.instance.StartGame(true);
            Close();
        }

        if (GameManager.instance.currentLevel == 2 || GameManager.instance.currentLevel > 3)
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

        if (GameManager.instance.currentLevel == 3)
        {
            if (Player.instance.currentHealth < 9)
            {
                Player.instance.ReturnSwimPos();
                Player.instance.ChangeSwimState();
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
        AudioManager.instance.StopSFX(9);
        AudioManager.instance.PlaySFX(8);
        GameManager.instance.StartDropCat();
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
        AudioManager.instance.PlaySFX(8);
        this.title.text = "You Lose!";
        backToMenuBtn.SetActive(true);
        replayButton.SetActive(false);
        nextLevelButton.SetActive(false);
    }

    public void HandleBackToMenu()
    {
        GameManager.instance.CloseScreen(ScreenKeys.RESULT_SCREEN);
        GameManager.instance.HandleBackToMenu();
    }
}
