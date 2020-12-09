﻿using System;
using PlayerBehaviors;
using PlayerState;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LineRenderer))]

public class PlayerInputAndPathHandler: IInitializable
{
    
    readonly TickableManager _tickableManager; 
    readonly EnemyObservable.Settings _enemyObservable;
    readonly PlayerFacade _player;
    private LineRenderer _lineRenderer;
    private IDisposable _ınputObservable;

    //RaycastSettings
    readonly Camera _camera;
    readonly LayerMask layer_mask;
    private RaycastHit _enemyHit;

    private PlayerStateManager _stateManager;
    PlayerInputAndPathHandler(PlayerFacade player,TickableManager tickableManager
        ,EnemyObservable.Settings enemyObservable,PlayerStateManager stateManager)
    {
        _player = player;
        _tickableManager = tickableManager;
        _enemyObservable = enemyObservable;
        _stateManager = stateManager;
        _camera=Camera.main;
        layer_mask = LayerMask.GetMask("Enemy");
        _enemyObservable._targetedEnemyList.Add(_player.gameObject);
    }
    
    public void Initialize()
    {
        _lineRenderer = _player.Linerenderer;
        _ınputObservable=_tickableManager.TickStream.Select(x => Input.touches)
            .Where(x =>
            {
                if (_stateManager.CurrentState!= PlayerStateManager.PlayerStates.IdleState | PathAlreadyCreated())
                {return false;}
                foreach (var touch in x)
                {
                        switch (touch.phase)
                        {
                               
                                case TouchPhase.Began:
                                    return CheckRayCast();
                        } 
                }

                return false;
            }).Subscribe(x => DetectEnemy(_enemyHit));
    }

    
    private void DetectEnemy(RaycastHit hit)
    {

        GameObject hitGO = hit.collider.gameObject;
        EnemyFacade hitFacade = hitGO.GetComponent<EnemyFacade>();
        
        if (IsLastHitObject())
        {
            hitFacade.ısAlreadyTargeted = false;
            _enemyObservable._targetedEnemyList.RemoveAt(_enemyObservable._targetedEnemyList.LastIndex());
        }
        else if ( !hitFacade.ısAlreadyTargeted)
        {
            hitFacade.ısAlreadyTargeted = true;
            _enemyObservable._targetedEnemyList.Add(hitGO); 
        }
        _lineRenderer.positionCount = _enemyObservable._targetedEnemyList.Count;
        DisplayLineDestination();
        
    }

    private void DisplayLineDestination()
    {
        for (int j = 0; j < _enemyObservable._targetedEnemyList.Count; j++)
        {
            var newPos=new Vector3(_enemyObservable._targetedEnemyList[j].transform.position.x,
                _enemyObservable._targetedEnemyList[j].transform.position.y+1,
                _enemyObservable._targetedEnemyList[j].transform.position.z);
            
            _lineRenderer.SetPosition(j,newPos);
        }
        
    }

    #region Conditions

    private bool CheckRayCast()
    {
        Ray camRay = _camera.ScreenPointToRay(Input.touches[0].position);
        if (Physics.Raycast(camRay, out var hit, 150, layer_mask))
        {
            _enemyHit = hit;
            return true;
        }
        return false;
    }

    private bool IsLastHitObject()
    {
        if (_enemyObservable._targetedEnemyList[_enemyObservable._targetedEnemyList.LastIndex()]==_enemyHit.collider.gameObject)
        {
            
            return true;
        }

        return false;
    }

    private bool PathAlreadyCreated()
    {
        if (_enemyObservable._targetedEnemyList.Count-1==_enemyObservable._totalEnemyCount)
        {
            return true;
        }

        return false;
    }

    #endregion
  
}

