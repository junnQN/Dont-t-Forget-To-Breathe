using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBucket : MonoBehaviour
{
    [SerializeField] private bool canFall = false;
    public float fallForce = 5f;
    

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canFall )
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.AddForce(Vector2.left * fallForce, ForceMode2D.Impulse);
                    
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canFall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canFall = false;
            
        }
    }

    
}
