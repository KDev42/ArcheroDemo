using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject stick;
    [SerializeField] TMP_Text CountdownTxt;
    [SerializeField] GameObject loadScrean;

    [Inject] EventManager eventManager;

    private int counter;

    private void Awake()
    {
        eventManager.startCountdown += StartCountdown;
        eventManager.changeLevel += OnLoadScrean;
        eventManager.heroKilled += OnLoadScrean;
    }

    private void StartCountdown()
    {
        CountdownTxt.gameObject.SetActive(true);
        stick.SetActive(false);

        counter = 3;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (counter > 0)
        {
            CountdownTxt.text = counter.ToString();
            counter--;
            yield return new WaitForSeconds(1);
        }

        eventManager.EndCountdown();
        CountdownTxt.gameObject.SetActive(false);
        stick.SetActive(true);
    }

    private void OnLoadScrean()
    {
        loadScrean.SetActive(true);
        StartCoroutine(OffLoadScrean());
    }
    IEnumerator OffLoadScrean()
    {
        yield return new WaitForSeconds(3);
        loadScrean.SetActive(false);
    }
}
