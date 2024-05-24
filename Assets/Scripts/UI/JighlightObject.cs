using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JighlightObject : MonoBehaviour
{
    [SerializeField] private Sprite newSprite;
    [SerializeField] private Sprite original;
    [SerializeField] private GameObject key;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || (GameManager.instance.isWater && gameObject.tag != "FlushButton") || (!GameManager.instance.isWater && gameObject.tag == "FlushButton"))
        {
            return;
        }

        ChangeSprite(newSprite); // Thay đổi material thành highlight material
        key.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra xem có chạm vào nhân vật không
        {
            ChangeSprite(original); // Thay đổi material về material ban đầu
            key.SetActive(false);
        }
    }

    void ChangeSprite(Sprite _sprite)
    {
        spriteRenderer.sprite = _sprite; // Thay đổi sprite của đối tượng thành sprite mới
    }
}
