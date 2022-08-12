using UnityEngine;
using Zenject;

public class SimpleSceneInstaller : MonoInstaller
{
    public Camera mainCamera;
    public override void InstallBindings()
    {
        Container.BindInstance(mainCamera).AsSingle();
    }
}