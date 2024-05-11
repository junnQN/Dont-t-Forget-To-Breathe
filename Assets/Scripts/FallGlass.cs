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
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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
            Debug.Log("FallGlass" + Player.instance.IsGroundDetected());
            if (!Player.instance.IsGroundDetected())
            {
                isFallGlass = true;
                if (other.transform.rotation.y < 0)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -180, transform.rotation.eulerAngles.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
                }
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                anim.SetBool("FallGlass", true);
            }
        }
    }

    public void OnGlassFallen()
    {
        water.SetActive(true);
    }
}
