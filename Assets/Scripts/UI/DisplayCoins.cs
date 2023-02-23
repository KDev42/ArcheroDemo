using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class DisplayCoins : MonoBehaviour
{
    [SerializeField] TMP_Text counterTxt;

    [Inject] EventManager eventManager;

    private int counter = -1;

    private void Start()
    {
        ChangeDisplay();
        eventManager.addCoins += ChangeDisplay;
    }

    private void ChangeDisplay()
    {
        counter++;
        counterTxt.text = counter.ToString();
    }
}
