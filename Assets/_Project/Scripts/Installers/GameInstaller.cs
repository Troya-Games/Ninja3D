using System;
using Miscs;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {

        /// <summary>
        /// MonoBehavior olarak, sahneden verilerek yüklenecek olan şeyler burada verilecek
        /// </summary>
        
        public SceneMonoSettings _SceneMonoSettings;
        public override void InstallBindings()
        {
            GameSignalsInstaller.Install(Container); //Signal Containerini yükle

            Container.BindInterfacesAndSelfTo<EnemyObservable>().AsSingle();
            Container.BindInterfacesTo<FinalEnemySetter>().AsSingle();
            Container.BindInterfacesTo<UIManager>().AsSingle();
            Container.BindInstance(_SceneMonoSettings.UImanagerStateUISettings).AsSingle();
            Container.BindInstance(_SceneMonoSettings.UIManagerSkullUISettings).AsSingle();
            Container.BindInstance(_SceneMonoSettings.UIManagerParticleEffects).AsSingle();
            Container.BindInstance(_SceneMonoSettings.EnemyObservableSettings).AsSingle();
            Container.BindInstance(_SceneMonoSettings.FinalEnemySettings).AsSingle();

            Container.Bind<LevelLoader>().AsSingle().NonLazy();


        }

        [Serializable]
        public class SceneMonoSettings
        {
            public UIManager.StateUISettings UImanagerStateUISettings;
            public UIManager.SkullUISettings UIManagerSkullUISettings;
            public UIManager.ParticleEffects UIManagerParticleEffects;
            public EnemyObservable.Settings EnemyObservableSettings;
            public FinalEnemySetter.Settings FinalEnemySettings;
        }

   
    
      
    }


}
 