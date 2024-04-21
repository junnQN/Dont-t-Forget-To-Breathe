using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGlass : MonoBehaviour
{
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("FallGlass",true);
        }
    }
}
