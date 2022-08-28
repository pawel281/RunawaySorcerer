using UnityEngine;
using Zenject;

public class SimpleSceneInstaller : MonoInstaller
{
    public Camera mainCamera;
    public PlayerCombat playerCombat;
    public ViewController viewController;
    public CreateSpellUISelector spellSelector;


    public override void InstallBindings()
    {
        Container.BindInstance(mainCamera).AsSingle();
        Container.BindInstance(viewController).AsSingle();
        Container.BindInstance(spellSelector).AsSingle();
        Container.BindInstance(playerCombat).AsSingle();
        Container.Bind<MagicShield>().FromComponentInChildren();
    }
}