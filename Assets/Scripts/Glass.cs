using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public static Glass instance;

    public bool fall=false;
    
    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else 
            instance = this;
    }

    
    private void Update()
    {
        if (transform.rotation.y==180)
        {
            fall = true;
        }
        else if(transform.rotation.y==0)
        {
            fall = false;
        }
    }
}
