using System;
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
         .Select(x => _enemyObservable._currentTarget)
         .Where(x => x == _enemyObservable._totalEnemyCount & _stateEnum== StateEnum.nonsetted )
         .Subscribe(x =>
         {
            _enemyObservable._targetedEnemyList[1].tag = "Final";
            SetFinalEnemy();
            
         });
   }

   private void SetFinalEnemy()
   {
      var _lastEnemy = _enemyObservable._targetedEnemyList[1].gameObject.transform;
      _stateEnum = StateEnum.setted;

     _settings.FinalEnemy.transform.SetPositionAndRotationTo(_lastEnemy);
     _settings.FinalParticle.transform.SetPositionAndRotationTo(_lastEnemy);
     _settings.FinalParticle.transform.SetOnlyLocalYValue(-1.5f);
      
   }
   
   private enum StateEnum
   {
      nonsetted,
      setted
   }
}
