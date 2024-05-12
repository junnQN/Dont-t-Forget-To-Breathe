using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            spriteRenderer.sprite = sprite1;
        }
        else if (Input.GetKey(KeyCode.O))
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite0;
        }
    }
}
