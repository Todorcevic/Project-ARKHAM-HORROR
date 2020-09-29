using UnityEngine;
using Zenject;

namespace CardManager
{
    public class BindInstallerMono : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DeckBuildManager>().AsSingle();
        }
    }
}