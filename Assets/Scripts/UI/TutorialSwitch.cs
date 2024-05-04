using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSwitch : MonoBehaviour
{
    public static TutorialSwitch instance;

    [SerializeField] private GameObject UI_Game;
    [SerializeField] private GameObject tutorialObject;
    public Sprite[] images;
    private int currentIndex = 0;
    private SpriteRenderer spriteRenderer;
    public bool isTutorial=false;

    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else 
            instance = this;
    }
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
        UpdateImage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currentIndex<3)
            {
                currentIndex = (currentIndex + 1) % images.Length;
                UpdateImage();
            }
            else
            {
                isTutorial = false;
                tutorialObject.SetActive(false);
                UI_Game.SetActive(false);
                Hand.instance.canPlay = true;
            }
        }
    }

    private void UpdateImage()
    {
        if (isTutorial&&images.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = images[currentIndex];
        }
        
    }
}
