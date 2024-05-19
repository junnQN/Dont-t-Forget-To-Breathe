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
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
