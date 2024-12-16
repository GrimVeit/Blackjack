using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetView : View
{
    public event Action OnClickToPlayButton;
    public event Action OnClickToDoubleBetButton;
    public event Action OnClickToMaxBetButton;

    [SerializeField] private TextMeshProUGUI textBet;

    [SerializeField] private Button buttonDoubleBet;
    [SerializeField] private Button buttonMaxBet;

    [SerializeField] private Button buttonStartPlay;

    public void Initialize()
    {
        buttonMaxBet.onClick.AddListener(HandleClickToMaxBetButton);
        buttonDoubleBet.onClick.AddListener(HandleClickToDoubleBetButton);
        buttonStartPlay.onClick.AddListener(HandleClickToPlayButton);
    }

    public void Dispose()
    {
        buttonMaxBet.onClick.RemoveListener(HandleClickToMaxBetButton);
        buttonDoubleBet.onClick.RemoveListener(HandleClickToDoubleBetButton);
        buttonStartPlay.onClick.RemoveListener(HandleClickToPlayButton);
    }

    public void DisplayChangeBet(int bet)
    {
        textBet.text = bet.ToString();
    }

    public void ActivateStartButton()
    {
        buttonStartPlay.gameObject.SetActive(true);
    }

    public void DeactivateStartButton()
    {
        buttonStartPlay.gameObject.SetActive(false);
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

    private void HandleClickToPlayButton()
    {
        OnClickToPlayButton?.Invoke();
    }

    #endregion
}
