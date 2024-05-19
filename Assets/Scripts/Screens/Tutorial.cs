using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : BaseScreen
{
    public Sprite[] level1;
    public Sprite[] level2;
    public Sprite[] level3;
    public Sprite[] level4;
    public Sprite[] level5;
    public Sprite[][] listTutorials;
    public int currentIndex = 0;

    private void Awake()
    {
        Debug.Log("Tutorial Awake");
        listTutorials = new Sprite[5][];
        listTutorials[0] = level1;
        listTutorials[1] = level2;
        listTutorials[2] = level3;
        listTutorials[3] = level4;
        listTutorials[4] = level5;
    }

    public override void Open()
    {
        base.Open();
        currentIndex = 0;

        if (listTutorials[GameManager.instance.currentLevel - 1].Length == 0)
        {
            Debug.Log("Tutorial is completed");
            Close();
            return;
        }
        ShowImage(GameManager.instance.currentLevel, 0);
    }

    public override void Close()
    {
        base.Close();
        GameManager.instance.StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsCompleteTutorial())
            {
                ShowImage(GameManager.instance.currentLevel, currentIndex + 1);
            }
            else
            {
                Close();
            }
        }
    }

    private void ShowImage(int level, int index)
    {
        var images = listTutorials[level - 1];
        if (images.Length <= index) return;

        GetComponent<Image>().sprite = images[index];
        currentIndex = index;
    }

    private bool IsCompleteTutorial()
    {
        return currentIndex >= listTutorials[GameManager.instance.currentLevel - 1].Length - 1;
    }
}
