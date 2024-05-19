using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private float startX;
    [SerializeField] private float endX;

    [SerializeField] private float speed = 1;

    public Action onCollisionBox;

    Tween moveTween;

    [SerializeField] private GameObject waterFall;

    public void Init(Action onComplete)
    {
        if (GameManager.instance.currentLevel == 2)
        {
            AudioManager.instance.PlaySFX(11);
        }
        else
        {
            AudioManager.instance.StopSFX(11);
        }

        moveTween?.Kill();
        transform.localPosition = new Vector3(startX, transform.localPosition.y, transform.localPosition.z);
        moveTween = transform.DOLocalMoveX(endX, speed).OnComplete(() => onComplete());
    }

    public void ShowWaterFall()
    {
        waterFall.SetActive(true);
    }

    public void HideWaterFall()
    {
        waterFall.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D" + other.gameObject.tag);
        if (other.gameObject.tag == "Box")
        {
            AudioManager.instance.StopSFX(11);
            onCollisionBox?.Invoke();
        }
    }

    private void Update()
    {
        if (!waterFall.activeSelf) return;

        var boxCollider = GetComponent<BoxCollider2D>();
        // Lấy ra thông tin về kích thước và vị trí của BoxCollider2D
        Vector2 size = boxCollider.size;
        Vector2 center = (Vector2)transform.position + boxCollider.offset;

        // Kiểm tra overlap với các Collider 2D trong BoxCollider2D
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, size, 0f);

        // Kiểm tra từng Collider 2D có overlap với Collider 2D của đối tượng hay không
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Collider" + collider.tag);
            if (collider != null && collider.CompareTag("Water") && collider != boxCollider)
            {
                waterFall.SetActive(false);
            }
        }
    }
}
