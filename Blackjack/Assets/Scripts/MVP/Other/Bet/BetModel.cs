using System;
using UnityEngine;

public class BetModel
{
    public int CurrentBet { get; private set; }

    public event Action OnActivateGameButton;
    public event Action OnDeactivateGameButton;

    public event Action<float> OnCountForMaxBet;
    public event Action<float> OnChangeBet;
    public event Action<float> OnChangeWin;

    private float minBet, maxBet;

    private IMoneyProvider moneyProvider;

    private bool isActive = false;

    public BetModel(int minBet, int maxBet, IMoneyProvider moneyProvider)
    {
        this.minBet = minBet;
        this.maxBet = maxBet;
        this.moneyProvider = moneyProvider;
    }

    public void AddBet(int bet)
    {
        if (!isActive) return;

        moneyProvider.SendMoney(-bet);
        CurrentBet += bet;
        OnChangeBet?.Invoke(CurrentBet);

        if(CurrentBet >= minBet && CurrentBet <= maxBet)
        {
            OnActivateGameButton?.Invoke();
        }
        else
        {
            OnDeactivateGameButton?.Invoke();
        }
    }

    public void RemoveBet(int bet)
    {
        if (!isActive) return;

        moneyProvider.SendMoney(bet);
        CurrentBet -= bet;
        OnChangeBet?.Invoke(CurrentBet);

        if (CurrentBet >= minBet && CurrentBet <= maxBet)
        {
            OnActivateGameButton?.Invoke();
        }
        else
        {
            OnDeactivateGameButton?.Invoke();
        }
    }

    public void ClearBet()
    {
        if (!isActive) return;

        CurrentBet = 0;
        OnChangeBet?.Invoke(CurrentBet);

        if (CurrentBet >= minBet && CurrentBet <= maxBet)
        {
            OnActivateGameButton?.Invoke();
        }
        else
        {
            OnDeactivateGameButton?.Invoke();
        }
    }

    public void ReturnBet()
    {
        if (!isActive) return;

        moneyProvider.SendMoney(CurrentBet);
        CurrentBet = 0;
        OnChangeBet?.Invoke(CurrentBet);

        if (CurrentBet >= minBet && CurrentBet <= maxBet)
        {
            OnActivateGameButton?.Invoke();
        }
        else
        {
            OnDeactivateGameButton?.Invoke();
        }
    }

    public void MaxBet()
    {
        if (!isActive) return;

        var bet = moneyProvider.GetMoney() - CurrentBet;

        if(bet >= maxBet - CurrentBet)
        {
            OnCountForMaxBet?.Invoke(maxBet - CurrentBet);
        }

        if(bet >= minBet && bet < maxBet)
        {
            OnCountForMaxBet?.Invoke(bet);
        }
    }

    private void MaxBetDouble()
    {
        if (!isActive) return;

        Debug.Log("hjnkfmsd");

        var bet = moneyProvider.GetMoney() - CurrentBet;

        if(bet >= CurrentBet * 2)
        {
            OnCountForMaxBet?.Invoke(CurrentBet);
        }
    }

    public void DoubleBet()
    {
        MaxBetDouble();
    }

    public bool IsAvailable(int bet)
    {
        return bet <= maxBet - CurrentBet;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
