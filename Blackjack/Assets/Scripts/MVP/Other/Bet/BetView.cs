using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetView : View
{
    public event Action OnClickToDoubleBetButton;
    public event Action OnClickToMaxBetButton;

    [SerializeField] private TextMeshProUGUI textBet;

    [SerializeField] private Button buttonDoubleBet;
    [SerializeField] private Button buttonMaxBet;

    public void Initialize()
    {
        buttonMaxBet.onClick.AddListener(HandleClickToMaxBetButton);
        buttonDoubleBet.onClick.AddListener(HandleClickToDoubleBetButton);
    }

    public void Dispose()
    {
        buttonMaxBet.onClick.RemoveListener(HandleClickToMaxBetButton);
        buttonDoubleBet.onClick.RemoveListener(HandleClickToDoubleBetButton);
    }

    public void DisplayChangeBet(int bet)
    {
        textBet.text = bet.ToString();
    }

    #region Input

    private void HandleClickToMaxBetButton()
    {
        OnClickToMaxBetButton?.Invoke();
    }

    private void HandleClickToDoubleBetButton()
    {
        OnClickToDoubleBetButton?.Invoke();
    }

    #endregion
}
