using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class FinalEnemySetter: IInitializable
{
   private Settings _settings;

   private EnemyObservable.Settings _enemyObservable;

   private TickableManager _tickableManager;

   private StateEnum _stateEnum;
   

      FinalEnemySetter(Settings settings,EnemyObservable.Settings enemyObservable,TickableManager tickableManager)
   {
      _settings = settings;
      _enemyObservable = enemyObservable;
      _tickableManager = tickableManager;
      
   }
   
   
   [Serializable]
   public class Settings
   {
      public GameObject FinalEnemy;
      public ParticleSystem FinalParticle;
   }

   public void Initialize()
   {
      _tickableManager.TickStream
         .Select(x => _enemyObservable._targetedEnemyList)
         .Where(x => x.Count == _enemyObservable._totalEnemyCount+1 & _stateEnum== StateEnum.nonsetted )
         .Subscribe(x =>
         {
            x.Last().tag = "Final"; 
            SetFinalEnemy();
         });
   }

   private void SetFinalEnemy()
   {
      var _lastEnemy = _enemyObservable._targetedEnemyList.Last().gameObject.transform;
      _stateEnum = StateEnum.setted;

     _settings.FinalEnemy.transform.SetPositionAndRotationTo(_lastEnemy);

      //_settings.FinalEnemy.transform.ResetLocal();

      _settings.FinalParticle.transform.SetPositionAndRotationTo(_lastEnemy);
   }
   
   private enum StateEnum
   {
      nonsetted,
      setted
   }
}
