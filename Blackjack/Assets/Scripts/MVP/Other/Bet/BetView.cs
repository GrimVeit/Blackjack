using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetView : View
{
    public event Action OnClickToMaxBetButton;

    [SerializeField] private TextMeshProUGUI textBet;
    [SerializeField] private Button buttonMaxBet;

    public void Initialize()
    {
        buttonMaxBet.onClick.AddListener(HandleClickInMaxBetButton);
    }

    public void Dispose()
    {
        buttonMaxBet.onClick.RemoveListener(HandleClickInMaxBetButton);
    }

    public void DisplayChangeBet(int bet)
    {
        textBet.text = bet.ToString();
    }

    #region Input

    private void HandleClickInMaxBetButton()
    {
        OnClickToMaxBetButton?.Invoke();
    }

    #endregion
}
