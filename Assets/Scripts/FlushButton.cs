using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushButton : MonoBehaviour
{
    public Sprite button;
    public Sprite buttonPressed;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public void Init()
    {
        spriteRenderer.sprite = button;
    }

    public void ChangeButtonState(bool isPressed)
    {
        spriteRenderer.sprite = isPressed ? buttonPressed : button;
    }
}
