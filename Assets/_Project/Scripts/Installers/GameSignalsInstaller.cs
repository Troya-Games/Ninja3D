using Events;
using Zenject;

namespace Installers
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
    
        public override void InstallBindings()
        {
        
        
            SignalBusInstaller.Install(Container); //Signal birimi extenjecte eklendi
            Container.DeclareSignal<PlayerSignals>().OptionalSubscriber(); ///Bu şekilde sinyaller eklenecek
        }
    }
}
