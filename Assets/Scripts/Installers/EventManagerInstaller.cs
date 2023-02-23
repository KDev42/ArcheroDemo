using Zenject;
using UnityEngine;

public class EventManagerInstaller : MonoInstaller
{
    [SerializeField] EventManager eventManager;

    public override void InstallBindings()
    {
        Container.Bind<EventManager>().FromInstance(eventManager).AsSingle();
        Container.QueueForInject(eventManager);
    }
}
