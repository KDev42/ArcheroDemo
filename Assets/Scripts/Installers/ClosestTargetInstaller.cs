using UnityEngine;
using Zenject;

public class ClosestTargetInstaller : MonoInstaller
{
    [SerializeField] ClosestTarget closestTarget ;

    public override void InstallBindings()
    {
        Container.Bind<ClosestTarget>().FromInstance(closestTarget).AsSingle();
        Container.QueueForInject(closestTarget);
    }
}
