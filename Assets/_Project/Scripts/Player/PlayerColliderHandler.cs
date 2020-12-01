using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PlayerBehaviors;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class PlayerColliderHandler: MonoBehaviour
{
   private Player _player;
   private ReactiveProperty<int> _currentEnemy;
   private int _currentEnemyInt=0;
   private PlayerMove _playerMove;
   private EnemyObservable.Settings _enemyObservable;

 [Inject]public void Construct(PlayerMove playerMove, EnemyObservable.Settings enemyObservable)
   {
       _playerMove = playerMove;
       _enemyObservable = enemyObservable;
   }
   

   

  private void Awake()
  {
      _currentEnemy.Subscribe(x => _playerMove._currentEnemy = _currentEnemy.Value);
      

  }

  private void Start()
  {
      this.OnTriggerEnterAsObservable()
          .Where(_ => _.gameObject.CompareTag("Enemy") & !_.gameObject.GetComponent<EnemyFacade>().IsDead &
                      _enemyObservable._deadEnemyCount < _enemyObservable._totalEnemyCount)
          .Subscribe(_ =>
          {

              if (!_.GetComponent<EnemyFacade>().IsDead)
              {
                  _.gameObject.GetComponent<EnemyFacade>().IsDead = true;
                  _playerMove._moveEnum = PlayerMove.MoveEnum.ıdle;
                  _enemyObservable._deadEnemyCount++;
                  _currentEnemy.Value++;

              }
          });
  }

  private void Update()
  {
     
  }
}
