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

    [SerializeField] private GameObject buttonStartPlay;

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

    public void DisplayChangeBet(float bet)
    {
        textBet.text = bet.ToString();
    }

    public void ActivateStartButton()
    {
        buttonStartPlay.SetActive(true);
    }

    public void DeactivateStartButton()
    {
        buttonStartPlay.SetActive(false);
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
