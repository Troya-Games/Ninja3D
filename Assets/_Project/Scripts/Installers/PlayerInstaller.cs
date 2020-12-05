using System;
using PlayerBehaviors;
using PlayerState;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

/// <summary>
/// Playerin kendi içinde verilecek bağımlılıklar buradan yüklenecek
/// </summary>

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;
        public override void InstallBindings()
        {
            Container.Bind<Player>().AsSingle()
                .WithArguments(_settings.Rigidbody,_settings.Animator
                    ,_settings.LineRenderer,_settings.Collider,_settings.MeshRenderer,_settings.Weapons);
            Container.BindInterfacesAndSelfTo<PlayerInputAndPathHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle();
            StateManagerInstall();
            Container.Bind<PlayerColliderHandler>().AsSingle();

        }

        void StateManagerInstall()
        {
            Container.BindInterfacesAndSelfTo<PlayerStateManager>().AsSingle();
            Container.Bind<IdleState>().AsSingle();
            Container.Bind<RunningState>().AsSingle();
            Container.Bind<DeadState>().AsSingle();
            Container.Bind<FinalState>().AsSingle();
            Container.Bind<FinishState>().AsSingle();
        }
    
        [Serializable]
        public class Settings
        {
            public Rigidbody Rigidbody;
            public Animator Animator;
            public LineRenderer LineRenderer;
            public BoxCollider Collider;
            public SkinnedMeshRenderer MeshRenderer;
            public GameObject[] Weapons;
        }
    }
}
