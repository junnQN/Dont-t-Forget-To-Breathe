using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public void HandleDieComplete()
    {
        GameManager.instance.HandleGameLose();
    }
}
