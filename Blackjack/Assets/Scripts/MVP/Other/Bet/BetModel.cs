using System;
using UnityEngine;

public class BetModel : MonoBehaviour
{
    public event Action<int> OnCountForMaxBet;
    public event Action<int> OnChangeBet;

    private int minBet, maxBet;

    private int currentBet = 0;
    private IMoneyProvider moneyProvider;

    public BetModel(int minBet, int maxBet, IMoneyProvider moneyProvider)
    {
        this.minBet = minBet;
        this.maxBet = maxBet;
        this.moneyProvider = moneyProvider;
    }

    public void AddBet(int bet)
    {
        moneyProvider.SendMoney(-bet);
        currentBet += bet;
        OnChangeBet?.Invoke(currentBet);
    }

    public void RemoveBet(int bet)
    {
        moneyProvider.SendMoney(bet);
        currentBet -= bet;
        OnChangeBet?.Invoke(currentBet);
    }

    public void ClearBet()
    {
        moneyProvider.SendMoney(currentBet);
        currentBet = 0;
        OnChangeBet?.Invoke(currentBet);
    }

    public void MaxBet()
    {
        OnCountForMaxBet?.Invoke(maxBet - currentBet);
    }

    public bool IsAvailable(int bet)
    {
        return bet <= maxBet - currentBet;
    }
}
