using System;
using Miscs;
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
            Container.BindInstance(_SceneMonoSettings.UIMANAGER_settings).AsSingle();
            Container.BindInstance(_SceneMonoSettings.EnemyObservableSettings).AsSingle();
          
        }

        [Serializable]
        public class SceneMonoSettings
        {
            public UIManager.Settings UIMANAGER_settings;
            public EnemyObservable.Settings EnemyObservableSettings;
        }

   
    
      
    }


}
 