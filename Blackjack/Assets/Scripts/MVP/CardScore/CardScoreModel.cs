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

    public int ScorePlayer { get; private set; }
    public int ScoreDealer { get; private set; }

    public void AddScore_Player(int score)
    {
        ScorePlayer += score;

        OnChangeScore_Player?.Invoke(ScorePlayer);

        if(ScorePlayer > 21)
        {
            OnLose?.Invoke();
        }
    }

    public void AddScore_Dealer(int score)
    {
        ScoreDealer += score;

        OnChangeScore_Dealer?.Invoke(ScoreDealer);

        if (ScoreDealer > 21)
        {
            OnWin?.Invoke();
        }
    }

    public void CalculateResult()
    {
        if(ScorePlayer > ScoreDealer)
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
        ScorePlayer = 0;
        OnChangeScore_Player.Invoke(ScorePlayer);

    }

    public void ClearScore_Dealer()
    {
        ScoreDealer = 0;
        OnChangeScore_Dealer.Invoke(ScoreDealer);
    }
}
