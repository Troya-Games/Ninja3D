using System;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
namespace Miscs
{
    
    public class UIManager: IInitializable
    {
        readonly EnemyObservable.Settings _enemyObservable;
        readonly SkullUISettings _skullUISettings;

        private TickableManager _tickableManager;
        UIManager(SkullUISettings skullUISettings,EnemyObservable.Settings enemyObservable,TickableManager tickableManager)
        {
            _skullUISettings = skullUISettings;
            _enemyObservable = enemyObservable;
            _tickableManager = tickableManager;

        }

        
        public void Initialize()
        {
            _tickableManager.TickStream.Subscribe(x =>
            {
                _skullUISettings._targetedEnemyText.text = 
                    _enemyObservable._deadEnemyCount 
                    + "/" +
                    _enemyObservable._totalEnemyCount;
            });

        }
    

    
    
    
    [Serializable]
    public class StateUISettings
    {
        public GameObject _gamePreUI;
        public GameObject _gameInUI;
        public GameObject _gameDeadUI;
        public GameObject _gameFinishUI;
    }
    
    [Serializable]
    public class  ParticleEffects
    {
        public ParticleSystem _finishParticle;
    }

    [Serializable]
    public class SkullUISettings
    {
        public TextMeshProUGUI _targetedEnemyText;
    }
    }
}
