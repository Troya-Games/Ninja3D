using System;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public Settings _settings;
    public EnemyDeathHandler.Settings _deathSettings;
    public EnemySkullHandler.Settings _skullSettings;
  
    public override void InstallBindings()
    {
        Container.Bind<EnemyModel>().AsSingle().WithArguments(_settings.Rigidbody, _settings.Animator
            ,_settings.SkinnedMeshRenderer);
        Container.BindInterfacesTo<EnemyDeathHandler>().AsSingle();
        Container.BindInstance(_deathSettings).AsSingle();
        Container.BindInstance(_skullSettings).AsSingle();
        Container.BindInterfacesTo<EnemySkullHandler>().AsSingle();

    }
    
    [Serializable]
    public class Settings
    {
        public Rigidbody Rigidbody;
        public Animator Animator;
        public SkinnedMeshRenderer SkinnedMeshRenderer;
    }
    
   
}