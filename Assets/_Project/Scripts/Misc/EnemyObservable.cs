using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObservable
{
    

    [Serializable]
    public class Settings
    {
        public List<GameObject> _targetedEnemyList;
        public int _currentTarget;
        public float _deadEnemyCount;
        public float _totalEnemyCount;
    }
}
