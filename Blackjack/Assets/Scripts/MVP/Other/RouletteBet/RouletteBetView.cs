using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteBetView : View
{
    [SerializeField] private Button buttonRed;
    [SerializeField] private Button buttonBlack;
    [SerializeField] private Button buttonGetBonus;

    public void Initialize()
    {
        buttonRed.onClick.AddListener(()=> OnChooseRed?.Invoke());
        buttonBlack.onClick.AddListener(()=> OnChooseBlack?.Invoke());
        buttonGetBonus.onClick.AddListener(()=> OnGetBonus?.Invoke());
    }

    public void Dispose()
    {
        buttonRed.onClick.RemoveListener(() => OnChooseRed?.Invoke());
        buttonBlack.onClick.RemoveListener(() => OnChooseBlack?.Invoke());
        buttonGetBonus.onClick.RemoveListener(() => OnGetBonus?.Invoke());
    }

    public void Activate()
    {
        buttonBlack.gameObject.SetActive(true);
        buttonRed.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        buttonBlack.gameObject.SetActive(false);
        buttonRed.gameObject.SetActive(false);
    }

    #region Input

    public event Action OnChooseRed;
    public event Action OnChooseBlack;
    public event Action OnGetBonus;

    #endregion
}
