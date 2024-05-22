using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupperItem : MonoBehaviour
{
    [SerializeField] private GameObject key;
    public void Init()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (GameManager.instance.currentLevel != 3 || gameObject.tag == "FlushButton")) // Kiểm tra xem có chạm vào nhân vật không
        {
            key.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra xem có chạm vào nhân vật không
        {
            key.SetActive(false);
        }
    }
}
