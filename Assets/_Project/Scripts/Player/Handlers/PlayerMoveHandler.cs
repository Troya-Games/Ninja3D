using DG.Tweening;
using PlayerBehaviors;
using PlayerState;
using UniRx;
using Zenject;

public class PlayerMoveHandler : IInitializable
{
    private readonly EnemyObservable.Settings _enemyObservable;
    private readonly TickableManager _tickableManager;
    private readonly PlayerFacade _player;
    private readonly PlayerStateManager _stateManager;
    public MoveEnum _moveEnum;
    private readonly Sequence _sequence;

    private PlayerMoveHandler(EnemyObservable.Settings enemySettings, TickableManager tickableManager, PlayerFacade player,
        PlayerStateManager playerStateManager)
    {
        _enemyObservable = enemySettings;
        _tickableManager = tickableManager;
        _player = player;
        _stateManager = playerStateManager;
        _sequence = DOTween.Sequence();
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
        _player.transform.LookAt(_enemyObservable._targetedEnemyList[_enemyObservable._currentTarget].transform);
        _sequence.Append(_player.transform.DOMove(_enemyObservable._targetedEnemyList[_enemyObservable._currentTarget].transform.position,1.5f).SetEase(Ease.InQuint));
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

