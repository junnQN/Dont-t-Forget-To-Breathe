using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    public Smoke smoke;
    public void Init()
    {
        gameObject.SetActive(true);
        smoke.Init();
    }

    void Update()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        // Lấy ra thông tin về kích thước và vị trí của BoxCollider2D
        Vector2 size = boxCollider.size;
        Vector2 center = (Vector2)transform.position + boxCollider.offset;

        // Kiểm tra overlap với các Collider 2D trong BoxCollider2D
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f);

        // Kiểm tra từng Collider 2D có overlap với Collider 2D của đối tượng hay không
        foreach (Collider2D collider in colliders)
        {
            if (collider != null && (collider.CompareTag("WaterGlass") || collider.CompareTag("Water")) && collider != boxCollider)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
