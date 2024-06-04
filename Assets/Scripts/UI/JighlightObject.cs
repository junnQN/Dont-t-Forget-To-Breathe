using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JighlightObject : MonoBehaviour
{
    [SerializeField]private Sprite newSprite;
    [SerializeField] private Sprite original;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra xem có chạm vào nhân vật không
        {
            ChangeSprite(newSprite); // Thay đổi material thành highlight material
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra xem có chạm vào nhân vật không
        {
            ChangeSprite(original); // Thay đổi material về material ban đầu
        }
    }
    
    void ChangeSprite(Sprite _sprite)
    {
        spriteRenderer.sprite = _sprite; // Thay đổi sprite của đối tượng thành sprite mới
    }
}
