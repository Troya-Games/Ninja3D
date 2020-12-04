using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class rESTART : MonoBehaviour
{
   public Button _Button;
   public delegate void _ButtonDelegates();
   public static  event _ButtonDelegates restartEvent;

   private void Awake()
   {
      restartEvent += () =>
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      };     
      _Button.onClick.AddListener(restartEvent.Invoke);
   }
   
}
