using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using PlayerBehaviors;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LineRenderer))]

public class PlayerPathFinder: IInitializable
{
    readonly Player _player;
    readonly TickableManager _tickableManager;
    readonly LineRenderer _lineRenderer; 
    readonly EnemyObservable.Settings _enemyObservableSettings;
    
  
    
    //RaycastSettings
    readonly Camera _camera;
    readonly LayerMask layer_mask;
    private RaycastHit _enemyHit;
    readonly List<Vector3> _points=new List<Vector3>();
    readonly List<GameObject> _enemyList=new List<GameObject>();

    public readonly IObservable<List<GameObject>> _enemyObservable;

    PlayerPathFinder(Player player,TickableManager tickableManager,EnemyObservable.Settings enemyObservable)
    {
        _player = player;
        _tickableManager = tickableManager;
        _enemyObservableSettings = enemyObservable;
        _camera=Camera.main;
        _lineRenderer = _player.LineRenderer;
        _points.Add(_player.Position);
        layer_mask = LayerMask.GetMask("Enemy");
        _enemyObservable=_tickableManager.TickStream.Select(x => _enemyList);
    }
    
    public void Initialize()
    {
        _tickableManager.TickStream.Select(x => Input.touches)
            .Where(x =>
            {
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
    
    

    private bool CheckRayCast()
    {
        Ray camRay = _camera.ScreenPointToRay(Input.touches[0].position);
        if (Physics.Raycast(camRay, out var hit, 25, layer_mask))
        {
            _enemyHit = hit;
            return true;
        }
        return false;
    }
    
    private void DetectEnemy(RaycastHit hit)
    {

        GameObject hitGO = hit.collider.gameObject;
        EnemyFacade hitFacade = hitGO.GetComponent<EnemyFacade>();
        
        if (hitFacade.IsTargeted)
        {
            if (_enemyList[_enemyList.Count-1]==hit.collider.gameObject)
            {
                hitFacade.IsTargeted = false;
                _enemyList.RemoveAt(_enemyList.Count-1);
                _points.RemoveAt(_points.Count-1);
                _enemyObservableSettings._targetedEnemyCount -= 1;
            }
        }
        else
        {
            hitFacade.IsTargeted = true;
            _enemyList.Add(hitGO);
            _points.Add(hit.point);
            _enemyObservableSettings._targetedEnemyCount += 1;
        }
        _lineRenderer.positionCount = _points.Count;
        DisplayLineDestination();
        
    }

    private void DisplayLineDestination()
    {

        for (int j = 0; j < _points.Count; j++)
        {
            _lineRenderer.SetPosition(j,_points[j]);
        }
        
    }
    
}

