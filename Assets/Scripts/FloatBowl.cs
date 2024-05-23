using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBowl : MonoBehaviour
{
    public static FloatBowl instance;
    
    public float floatHeight = 1.0f; // Độ cao từ mặt nước
    public float bounceDamping = 0.5f; // Sự giảm của độ lềnh bềnh
    public float waterLevel = 0.0f; // Độ cao của mặt nước
    public float fallSpeed = 9.8f; // Tốc độ rơi
    public float groundLevel = 0.0f; // Độ cao của mặt đất

    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else 
            instance = this;
        
    }
    
    private void Update()
    {
        if((GameManager.instance.currentLevel==3||GameManager.instance.currentLevel==5)&&GameManager.instance.isPlaying) // Lấy vị trí hiện tại của vật thể
        {
            if(GameManager.instance.isWater==true)
            {
                Vector3 position = transform.position;

                // Tính toán độ cao từ mặt nước
                float height = Mathf.Sin(Time.time) * floatHeight + waterLevel;

                // Di chuyển vật thể đến vị trí mới với độ cao được tính toán
                position.y = height;

                // Áp dụng sự giảm lềnh bềnh
                float bounce = Mathf.Lerp(transform.position.y, position.y, bounceDamping * Time.deltaTime);
                position.y = bounce;

                // Cập nhật vị trí của vật thể
                transform.position = position;
            }
            else if(GameManager.instance.isWater==false)
            {
                Vector3 position = transform.position;

                // Tính toán vị trí mới của vật thể khi rơi xuống
                position.y -= fallSpeed * Time.deltaTime;

                // Kiểm tra nếu vật thể chạm đất
                if (position.y <= groundLevel)
                {
                    position.y = groundLevel; // Đặt lại vị trí của vật thể là mặt đất
                    // Có thể thêm hành động khác ở đây, ví dụ như phát âm thanh, hiệu ứng, v.v.
                }

                // Cập nhật vị trí của vật thể
                transform.position = position;
            }
        }
    }
    
    
}
