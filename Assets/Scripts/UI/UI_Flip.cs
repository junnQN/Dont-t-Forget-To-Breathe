using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Flip : MonoBehaviour
{
    public static UI_Flip instance;

    private FallGlass glass;
    
    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else 
            instance = this;
    }

    private void Start()
    {
        glass = GetComponentInParent<FallGlass>();
        if(glass.isFallGlass)
            Flip();
    }

    public void Flip()
    {
        transform.Rotate(0,180,0);
    }
}
