using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PlayerBehaviors;
using PlayerState;
using UnityEngine;

public class FinalState : IState
{
    private Player _player;
    private SliceController _sliceController;
    private FinalEnemySetter.Settings _finalEnemySetter;
    FinalState(Player player,SliceController sliceController,FinalEnemySetter.Settings finalEnemySetter)
    {
        _player = player;
        _sliceController = sliceController;
        _finalEnemySetter = finalEnemySetter;
    }
   
    public void EnterState()
    {
        _player.GetAnimator.Play("FINALState");
        DOTween.KillAll();
        _sliceController.enabled = true;
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
