using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesFactoryInstaller : MonoInstaller
{
    [SerializeField] EnemiesFactory enemiesFactory;

    public override void InstallBindings()
    {
        Container.Bind<EnemiesFactory>().FromInstance(enemiesFactory).AsSingle();
        Container.QueueForInject(enemiesFactory);
    }
}
