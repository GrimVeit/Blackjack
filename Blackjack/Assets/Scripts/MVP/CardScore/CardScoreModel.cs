using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScoreModel
{
    public event Action OnWin;
    public event Action OnLose;

    public event Action<int> OnChangeScore_Player;
    public event Action<int> OnChangeScore_Dealer;

    private int scorePlayer;
    private int scoreDealer;

    public void AddScore_Player(int score)
    {
        scorePlayer += score;

        OnChangeScore_Player?.Invoke(scorePlayer);

        if(scorePlayer > 21)
        {
            OnLose?.Invoke();
        }
    }

    public void AddScore_Dealer(int score)
    {
        scoreDealer += score;

        OnChangeScore_Dealer?.Invoke(scoreDealer);

        if (scoreDealer > 21)
        {
            OnWin?.Invoke();
        }
    }

    public void CalculateResult()
    {
        if(scorePlayer > scoreDealer)
        {
            OnWin?.Invoke();
        }
        else
        {
            OnLose?.Invoke();
        }
    }

    public void ClearScore_Player()
    {
        scorePlayer = 0;
        OnChangeScore_Player.Invoke(scorePlayer);

    }

    public void ClearScore_Dealer()
    {
        scoreDealer = 0;
        OnChangeScore_Dealer.Invoke(scoreDealer);
    }
}
