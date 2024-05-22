using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutScreen : BaseScreen
{
    public bool isHulk = false;
    public VideoController videoController;
    public override void Open()
    {
        base.Open();
        if (isHulk)
        {
            videoController.PlayHulkClip();
        }
        else
        {
            videoController.PlayExplodeClip();
        }
    }

    public override void Close()
    {
        base.Close();
    }
}
