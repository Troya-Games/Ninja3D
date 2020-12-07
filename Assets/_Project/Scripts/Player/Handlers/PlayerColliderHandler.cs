using System;
using DG.Tweening;
using PlayerBehaviors;
using PlayerState;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Collider))]
public class PlayerColliderHandler: MonoBehaviour
{
   private Player _player;
   private PlayerMoveHandler _playerMoveHandler;
   private EnemyObservable.Settings _enemyObservable;
   private PlayerStateManager _stateManager;
  
 [Inject]public void Construct(PlayerMoveHandler playerMoveHandler, Player player
     ,EnemyObservable.Settings enemyObservable,PlayerStateManager stateManager
    )
 {
       _player = player;
       _playerMoveHandler = playerMoveHandler;
       _enemyObservable = enemyObservable;
       _stateManager = stateManager;
       
 }

 
 private void Start()
  {
      
      this.OnTriggerEnterAsObservable()
          .Where(_ => _stateManager.CurrentState==PlayerStateManager.PlayerStates.RunningState && 
                      _.gameObject.CompareTag("Enemy") && 
                      !_.gameObject.GetComponent<EnemyFacade>().IsDead &&
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

      this.OnTriggerEnterAsObservable().Where(_ => _.gameObject.CompareTag("Final"))
          .Subscribe(_ =>
          {
              _stateManager.ChangeState(PlayerStateManager.PlayerStates.FinalState);
              _.gameObject.GetComponent<EnemyFacade>().MeshRenderer.enabled=false;
              _.gameObject.tag = "Dead";
          });
      this.OnTriggerEnterAsObservable()
          .Where(x => x.gameObject.CompareTag("DangerZone"))
          .Subscribe(x => _stateManager.ChangeState(PlayerStateManager.PlayerStates.DeadState));
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
