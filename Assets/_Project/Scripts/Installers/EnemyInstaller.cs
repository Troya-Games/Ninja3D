using System;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public Settings _settings;
    public override void InstallBindings()
    {
        Container.Bind<EnemyModel>().AsSingle().WithArguments(_settings.Rigidbody, _settings.Animator);
    }
    
    [Serializable]
    public class Settings
    {
        public Rigidbody Rigidbody;
        public Animator Animator;
    }
}