using System;
using UniRx;
using UnityEngine;
using Zenject;

public class EnemyObservable
{
    

    [Serializable]
    public class Settings
    {
        public float _targetedEnemyCount;
        public float _deadEnemyCount;
        public float _totalEnemyCount;
    }
}
