using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class rESTART : MonoBehaviour
{
   public Button _Button;
   public Button _Button1;
   public delegate void _ButtonDelegates();
   public static  event _ButtonDelegates restartEvent;
   public static  event _ButtonDelegates nextLevelEvent;

   private void Awake()
   {
      restartEvent += () =>
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      };
      nextLevelEvent += () =>
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      };
      _Button.onClick.AddListener(restartEvent.Invoke);
      _Button1.onClick.AddListener(nextLevelEvent.Invoke);
   }
   
}
