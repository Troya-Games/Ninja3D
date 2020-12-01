using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PlayerBehaviors;
using PlayerState;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerMove : IInitializable
{
    readonly EnemyObservable.Settings _enemySettings;
    readonly TickableManager _tickableManager;
    readonly PlayerFacade _player;
    readonly ReadOnlyReactiveProperty<List<GameObject>> _enemyList;
    readonly PlayerStateManager _stateManager;

    private PlayerPathFinder _playerPathFinder;

    public MoveEnum _moveEnum;

  

    public int _currentEnemy = 0;

    readonly Sequence _sequence;
    PlayerMove(EnemyObservable.Settings enem, TickableManager tickableManager, PlayerFacade player,
        PlayerPathFinder playerPathFinder, PlayerStateManager playerStateManager)
    {
        _enemySettings = enem;
        _tickableManager = tickableManager;
        _player = player;
        _playerPathFinder = playerPathFinder;
        _stateManager = playerStateManager;
        _enemyList = _playerPathFinder._enemyObservable.ToReadOnlyReactiveProperty();
        _sequence = DOTween.Sequence();

    }

    public void Initialize()
    {
        _tickableManager.TickStream.Select(x => _enemySettings._targetedEnemyCount)
            .Where(x => x == _enemySettings._totalEnemyCount & (_moveEnum != MoveEnum.finished))
            .Subscribe(x =>
            {
                
                MoveToEnemy();
                _stateManager.ChangeState(PlayerStateManager.PlayerStates.RunningState);
            });
    }

    private void MoveToEnemy()
    {
        if (_currentEnemy==_enemySettings._totalEnemyCount-1)
        {
            _stateManager.ChangeState(PlayerStateManager.PlayerStates.FinishState);
        }
        
        if (_moveEnum != MoveEnum.finished)
        {
            
            if (_moveEnum == MoveEnum.ıdle)
            {
                _sequence.Append(_player.transform.DOMove(_enemyList.Value[_currentEnemy].transform.position,2).SetEase(Ease.InQuint));
                _moveEnum = MoveEnum.moving;
            }   
        }
        
    }

   

    public enum MoveEnum
    {
        ıdle,
        moving,
        finished,
    }
    }

