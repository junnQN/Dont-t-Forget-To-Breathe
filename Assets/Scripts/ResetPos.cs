using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
   public static ResetPos instance;
   

   private void Awake()
   {
      if (instance != null)
         Destroy(instance.gameObject);
      else
         instance = this;

   }

   public void ResetPosition()
   {
      transform.position = new Vector2(-6.314319f, -1.751203f);
   }
}
