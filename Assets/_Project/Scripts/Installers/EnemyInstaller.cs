using System;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public Settings _settings;
    public EnemyDeathHandler.Settings _deathSettings;
  
    public override void InstallBindings()
    {
        Container.Bind<EnemyModel>().AsSingle().WithArguments(_settings.Rigidbody, _settings.Animator,_settings.SkinnedMeshRenderer);
        Container.BindInterfacesTo<EnemyDeathHandler>().AsSingle();
        Container.BindInstance(_deathSettings).AsSingle();
    
    }
    
    [Serializable]
    public class Settings
    {
        public Rigidbody Rigidbody;
        public Animator Animator;
        public SkinnedMeshRenderer SkinnedMeshRenderer;
    }
}