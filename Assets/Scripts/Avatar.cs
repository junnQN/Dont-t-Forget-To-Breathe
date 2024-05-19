using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    private Animator animator;
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;

    public Image frame;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            frame.sprite = sprite1;
        }
        else if (Input.GetKey(KeyCode.O))
        {
            frame.sprite = sprite2;
        }
        else
        {
            frame.sprite = sprite0;
        }
    }
}
