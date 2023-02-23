using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class DropCoins : MonoBehaviour
{
    [SerializeField] GameObject coinPref;
    [SerializeField] Transform target;
    [SerializeField] float duration;

    [Inject] EventManager eventManager;

    private void Start()
    {
        eventManager.enemyKilled += Spawn;
    }

    private void Spawn(Vector3 startPos)
    {
        Transform coin;
        Vector3 spawnScreenPotion = Camera.main.WorldToScreenPoint(startPos);

        coin = Instantiate(coinPref, startPos, new Quaternion(0, 0, 0, 0), transform).transform;

        //coin.localScale = startScale;
        coin.position = spawnScreenPotion;
        coin.rotation = transform.rotation;
        //coin.SetParent(parentCoins);
        //coin = Instantiate(coinPrefab, spawnScreenPotion, transform.rotation, parentCoins);

        Tween tween = coin.transform.DOMove(target.position, duration);
        //coin.DOScale(endCoinScale, duration);

        tween.OnComplete(() => ReachedTarget(coin));
    }

    private void ReachedTarget(Transform obj)
    {
        //EventManager.Instance.AddCoins();
        eventManager.AddCoin();
        Destroy(obj.gameObject);
    }
}
