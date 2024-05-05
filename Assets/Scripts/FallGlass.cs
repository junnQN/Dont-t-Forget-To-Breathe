using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallGlass : MonoBehaviour
{
    private Animator anim;
    public bool isFallGlass = false;

    public GameObject water;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void Init()
    {
        water.SetActive(false);
        anim.SetBool("FallGlass", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("FallGlass", true);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (anim.GetBool("FallGlass") == true)
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            var boundYPlayer = other.collider.bounds.size.y;
            var boundYGlass = GetComponent<BoxCollider2D>().bounds.size.y;
            if (other.transform.position.y - boundYPlayer / 2 > transform.position.y + boundYGlass / 2)
            {
                isFallGlass = true;
                if (other.transform.rotation.y == -180)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -180, transform.rotation.eulerAngles.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
                }

                anim.SetBool("FallGlass", true);
            }
        }
    }

    public void OnGlassFallen()
    {
        anim.SetBool("FallGlass", false);
        water.SetActive(true);
    }
}
