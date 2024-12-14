using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetPresenter : IBetProvider
{
    private BetModel model;
    private BetView view;

    public BetPresenter(BetModel model, BetView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    public void ActivateEvents()
    {
        view.OnClickToMaxBetButton += model.MaxBet;

        model.OnChangeBet += view.DisplayChangeBet;
    }

    public void DeactivateEvents()
    {
        model.OnChangeBet -= view.DisplayChangeBet;
    }

    #region Input

    public event Action<int> OnCountForMaxBet
    {
        add { model.OnCountForMaxBet += value; }
        remove { model.OnCountForMaxBet -= value; }
    }

    public void AddBet(int bet)
    {
        model.AddBet(bet);
    }

    public void RemoveBet(int bet)
    {
        model.RemoveBet(bet);
    }

    public void ClearBet()
    {
        model.ClearBet();
    }

    public bool IsAvailable(int bet)
    {
        return model.IsAvailable(bet);
    }

    #endregion
}

public interface IBetProvider
{
    bool IsAvailable(int bet);
}
