using DG.Tweening;
using PlayerBehaviors;
using PlayerState;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerMoveHandler : IInitializable
{
    public MoveEnum _moveEnum;

    private readonly EnemyObservable.Settings _enemyObservable;
    private readonly TickableManager _tickableManager;
    private readonly PlayerFacade _player;
    private readonly PlayerStateManager _stateManager;
    private readonly Sequence _moveSequence;
    private readonly Sequence _rotationSequence;

    private PlayerMoveHandler(EnemyObservable.Settings enemySettings, TickableManager tickableManager, PlayerFacade player,
        PlayerStateManager playerStateManager)
    {
        _enemyObservable = enemySettings;
        _tickableManager = tickableManager;
        _player = player;
        _stateManager = playerStateManager;
        _moveSequence = DOTween.Sequence();
        _rotationSequence = DOTween.Sequence();
    }

    public void Initialize()
    {
        _tickableManager.TickStream
            .Where(x => AllEnemiesTargeted() & CanMove())
            .Subscribe(x =>
            {
                _stateManager.ChangeState(PlayerStateManager.PlayerStates.RunningState);
                MoveToEnemy();
            });
    }

    private void MoveToEnemy()
    {
        _moveEnum = MoveEnum.moving;
        
        var CurrentEnemy = _enemyObservable._targetedEnemyList[_enemyObservable._currentTarget];
        
        Vector3 relativePos = CurrentEnemy.transform.position -  _player.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        _moveSequence.Append(_player.transform.DOMove(CurrentEnemy.transform.position,1.5f).SetEase(Ease.InQuint));
        _rotationSequence.Append(_player.transform.DORotate(toRotation.eulerAngles, 1));
    }
    
    
    #region Conditions
    
    private bool AllEnemiesTargeted()
    {
        if ((_enemyObservable._targetedEnemyList.LastIndex() == _enemyObservable._totalEnemyCount) )
        {
            return true;
        }
        return false;
    }
    
    private bool CanMove()
    {
        if ((_moveEnum == MoveEnum.canMove))
        {
            return true;
        }
        return false;
    }

    #endregion

    public enum MoveEnum
    {
        canMove,
        moving,
        finished,
    }
    }

