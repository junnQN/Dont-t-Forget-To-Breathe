using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseWaterButton : FlushButton
{
    public static ReleaseWaterButton instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void ChangeTag()
    {
        gameObject.tag = "Trash";
    }
}
