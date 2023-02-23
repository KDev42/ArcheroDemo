using UnityEngine;
using Zenject;

public class LevelDataInstaller : MonoInstaller
{
    [SerializeField] LevelData levelData;

    public override void InstallBindings()
    {
        Container.Bind<LevelData>().FromInstance(levelData).AsSingle();
        Container.QueueForInject(levelData);
    }
}
