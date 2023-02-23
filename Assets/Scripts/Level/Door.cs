using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Door : MonoBehaviour
{
    [SerializeField] LayerMask heroLayer;

    [Inject] EventManager eventManager;


    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & heroLayer) != 0)
        {
            eventManager.ChangeLevel();
        }
    }
}
