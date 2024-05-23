using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : BaseScreen
{
    public Sprite[] newspapers;
    public Image image;

    public void HandleQuitButton()
    {
        Application.Quit();
    }

    public void HandleNextButton()
    {
        AudioManager.instance.PlaySFX(8);
        GameManager.instance.StartDropCat();
        Close();
    }

    public override void Open()
    {
        UpdateUI();
        base.Open();
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
    }
}
