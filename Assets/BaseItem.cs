using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    protected Vector2 defaultPos;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    public void Init()
    {
        transform.position = defaultPos;
    }
}
