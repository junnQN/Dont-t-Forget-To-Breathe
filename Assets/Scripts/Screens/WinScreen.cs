using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : BaseScreen
{
    public Sprite[] newspapers;
    public Image image;

    [SerializeField]
    private GameObject nextLevelButton;

    [SerializeField] private GameObject backToMenuBtn;

    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public override void Open()
    {
        UpdateUI();
        base.Open();
        AudioManager.instance.playBGM = false;
        AudioManager.instance.PlaySFX(21);
    }

    public override void Close()
    {
        base.Close();
        AudioManager.instance.playBGM = true;
    }

    private void UpdateUI()
    {
        var currentLevel = GameManager.instance.currentLevel;
        if (currentLevel < 5)
        {
            image.sprite = newspapers[currentLevel - 1];
        }
        else
        {
            image.sprite = Player.instance.isHulk ? newspapers[5] : newspapers[4];
        }
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        if (GameManager.instance.currentLevel < 5)
        {
            nextLevelButton.SetActive(true);
            backToMenuBtn.SetActive(false);
        }
        else
        {
            nextLevelButton.SetActive(false);
            backToMenuBtn.SetActive(true);
        }
    }

    public void HandleBackToMenu()
    {
        GameManager.instance.CloseScreen(ScreenKeys.WIN_SCREEN);
        GameManager.instance.HandleBackToMenu();
    }

    public void HandleNextButton()
    {
        AudioManager.instance.PlaySFX(8);
        GameManager.instance.StartDropCat();
        Close();
    }
}
