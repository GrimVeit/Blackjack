using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteBetPresenter
{
    private RouletteBetModel model;
    private RouletteBetView view;

    public RouletteBetPresenter(RouletteBetModel rouletteBetModel, RouletteBetView rouletteBetView)
    {
        this.model = rouletteBetModel;
        this.view = rouletteBetView;
    }

    public void Initialize()
    {
        ActivateEvents();

        model.Initialize();
        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        model.Dispose();
        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnChooseBlack += model.SetBetBlack;
        view.OnChooseRed += model.SetBetRed;

        model.OnActivate += view.Activate;
        model.OnDeactivate += view.Deactivate;
    }

    private void DeactivateEvents()
    {
        view.OnChooseBlack -= model.SetBetBlack;
        view.OnChooseRed -= model.SetBetRed;

        model.OnActivate -= view.Activate;
        model.OnDeactivate -= view.Deactivate;
    }

    #region Input

    public event Action OnChooseBet
    {
        add { model.OnChooseBet += value; }
        remove { model.OnChooseBet -= value; }
    }

    public event Action OnSuccess
    {
        add { model.OnSuccess += value; }
        remove { model.OnSuccess -= value; }
    }

    public event Action OnFailure
    {
        add { model.OnFailure += value; }
        remove { model.OnFailure -= value; }
    }

    public void SetResult(RouletteSlotValue rouletteSlotValue)
    {
        model.SetResult(rouletteSlotValue.RouletteNumber.Color);
    }

    public void ActivateBet()
    {
        model.ActivateBet();
    }

    #endregion
}
