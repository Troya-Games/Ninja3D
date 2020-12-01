using PlayerBehaviors;
using PlayerState;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class PlayerColliderHandler: MonoBehaviour
{
   private Player _player;
   private PlayerMoveHandler _playerMoveHandler;
   private EnemyObservable.Settings _enemyObservable;
   private PlayerStateManager _stateManager;

 [Inject]public void Construct(PlayerMoveHandler playerMoveHandler, EnemyObservable.Settings enemyObservable,PlayerStateManager stateManager)
   {
       _playerMoveHandler = playerMoveHandler;
       _enemyObservable = enemyObservable;
       _stateManager = stateManager;
   }
 
  private void Awake()
  {
      this.OnTriggerEnterAsObservable()
          .Where(_ => _.gameObject.CompareTag("Enemy") & 
                      !_.gameObject.GetComponent<EnemyFacade>().IsDead &
                      !IsLastTarget())
          .Subscribe(_ =>
          {
              if (!_.GetComponent<EnemyFacade>().IsDead)
              {
                  _.gameObject.GetComponent<EnemyFacade>().IsDead = true;
                  _playerMoveHandler._moveEnum = PlayerMoveHandler.MoveEnum.canMove;
                  _enemyObservable._deadEnemyCount++;
                  if (  _enemyObservable._currentTarget==_enemyObservable._totalEnemyCount)
                  {
                      _playerMoveHandler._moveEnum = PlayerMoveHandler.MoveEnum.finished;
                      _stateManager.ChangeState(PlayerStateManager.PlayerStates.FinishState);
                      return;
                  }
                  _enemyObservable._currentTarget++;
              }
          });
  }

  #region Conditions
  
  private bool IsLastTarget()
  {
      if (_enemyObservable._deadEnemyCount < _enemyObservable._totalEnemyCount)
      {
          return false;
      }

      return true;
  }

  #endregion
  
}
