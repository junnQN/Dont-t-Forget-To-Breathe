using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public string screenName;
    public virtual void Open()
    {
        Debug.Log("<color=green>Open screen: " + screenName + "</color>");
        gameObject.SetActive(true);
        if (GameManager.instance.currentLevel == 5)
        {
            AudioManager.instance.StopSFX(22);
            AudioManager.instance.StopSFX(23);
        }
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
