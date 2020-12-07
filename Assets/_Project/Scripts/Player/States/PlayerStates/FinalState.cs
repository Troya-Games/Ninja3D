using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PlayerBehaviors;
using PlayerState;
using UnityEngine;

public class FinalState : IState
{
    private Player _player;
    private FinalEnemySetter.Settings _finalEnemySetter;
    FinalState(Player player,FinalEnemySetter.Settings finalEnemySetter)
    {
        _player = player;
      
        _finalEnemySetter = finalEnemySetter;
    }
   
    public void EnterState()
    {
         DOTween.KillAll();
        _player.RigidBody.ResetVelocity();
        _player.Position-=6* _player.Collider.transform.forward;
      
        _player.GetAnimator.Play("FINALState");
        _finalEnemySetter.FinalEnemy.SetActive(true);

    }

    public void ExitState()
    {
    }
    public  void FixedUpdate()
    {
    }

    public   void Update()
    {
        
    }
}
