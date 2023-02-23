using UnityEngine;
using Zenject;

public class LevelStartInstaller : MonoInstaller
{
    [SerializeField] LevelStart levelStart;

    public override void InstallBindings()
    {
        Container.Bind<LevelStart>().FromInstance(levelStart).AsSingle();
        Container.QueueForInject(levelStart);
    }
}
