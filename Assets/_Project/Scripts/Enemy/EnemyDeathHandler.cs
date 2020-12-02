using System;
using UnityEngine;
using UniRx;
using Zenject;

public class EnemyDeathHandler: IInitializable
{
    readonly EnemyModel _enemy;
    readonly IObservable<bool> _deathObservable;
    readonly Settings _settings;

    EnemyDeathHandler(EnemyModel enemy,Settings settings)
    {
        _enemy = enemy;
        _settings = settings;
        _deathObservable = Observable.EveryUpdate().Select(x => _enemy.IsDead).Where(x=>x.Equals(true));
    }

    public void Initialize()
    {
        _deathObservable.Subscribe(x =>
        {
            _enemy.Animator.Play("DeathState");
            _settings._deathParticle.Play();
            Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(y => _enemy.IsDead = false);
        });
    }

    [Serializable]
    public class Settings
    {
        public ParticleSystem _deathParticle;
    }
}
