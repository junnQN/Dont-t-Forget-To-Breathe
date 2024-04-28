using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance; 
    
    [Header("End screen")]
    public UI_FadeScreen fadeScreen;
    public GameObject endText;
    public GameObject restartButton;
    public GameObject winText;
    public GameObject nextLvButton;
    
   
    //[SerializeField] private GameObject inGameUI;

    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else 
            instance = this;
        
        //fadeScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        //SwitchTo(inGameUI);
    }

    private void Update()
    {
        
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;
            
            if(fadeScreen == false) 
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }
        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
                return;
        }
        //SwitchTo(inGameUI);
    }

    public void SwitchOnEndScreen()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();
    
    public void SwitchOnWinScreen()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.FadeOut();
        StartCoroutine(WinScreenCoroutine());
        
    }

    IEnumerator WinScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        winText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        nextLvButton.SetActive(true);
    }
}