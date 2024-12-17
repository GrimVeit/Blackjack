using System;
using UnityEngine;

public class BetModel
{
    public event Action OnActivateGameButton;
    public event Action OnDeactivateGameButton;

    public event Action<int> OnCountForMaxBet;
    public event Action<int> OnChangeBet;

    private int minBet, maxBet;

    private int currentBet = 0;
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
        currentBet += bet;
        OnChangeBet?.Invoke(currentBet);

        if(currentBet >= minBet && currentBet <= maxBet)
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
        currentBet -= bet;
        OnChangeBet?.Invoke(currentBet);

        if (currentBet >= minBet && currentBet <= maxBet)
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

        moneyProvider.SendMoney(currentBet);
        currentBet = 0;
        OnChangeBet?.Invoke(currentBet);

        if (currentBet >= minBet && currentBet <= maxBet)
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

        var bet = moneyProvider.GetMoney() - currentBet;

        if(bet >= maxBet - currentBet)
        {
            OnCountForMaxBet?.Invoke(maxBet - currentBet);
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

        var bet = moneyProvider.GetMoney() - currentBet;

        if(bet >= currentBet * 2)
        {
            OnCountForMaxBet?.Invoke(currentBet);
        }
    }

    public void DoubleBet()
    {
        MaxBetDouble();
    }

    public bool IsAvailable(int bet)
    {
        return bet <= maxBet - currentBet;
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
