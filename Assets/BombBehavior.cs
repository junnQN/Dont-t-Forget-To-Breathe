using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public FireBehavior fire;

    public void Init()
    {
        gameObject.SetActive(true);
        fire.Init();
    }
}
