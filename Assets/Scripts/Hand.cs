using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float startY = 8f;
    [SerializeField] private float stopY = -2.17f;
    [SerializeField] private float stopWaterY=0.5f;
    [SerializeField] private Sprite handnocat;
    [SerializeField] private Sprite handwithcat;
    [SerializeField] private GameObject player;
    public bool canPlay = false;

    private SpriteRenderer hand;

    public bool moveDown = true;

    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else 
            instance = this;
        
    }
    
    private void Start()
    {
        hand=GetComponent<SpriteRenderer>();
        startY = transform.position.y;
    }

    private void Update()
    {
        if (moveDown&&canPlay==true)
        {
            // Di chuyển vật thể từ trên xuống dưới
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (GameManager.instance.currentLevel == 3&&transform.position.y <= stopWaterY)
            {
                moveDown = false;
            }
            else if(GameManager.instance.currentLevel != 3&&transform.position.y <= stopY)
            {
                moveDown = false;
            }
        }
        else if(!moveDown)
        {
            player.SetActive(true);
            hand.sprite = handnocat;
            // Di chuyển vật thể từ dưới lên trên
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // Kiểm tra nếu vượt qua vị trí ban đầu
            if (transform.position.y >= startY)
            {
                // Đặt vị trí chính xác là vị trí ban đầu
                transform.position = new Vector3(transform.position.x, startY, transform.position.z);
                hand.sprite = handwithcat;
            }
        }
    }
}
