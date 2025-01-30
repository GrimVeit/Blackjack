using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteResultPresenter
{
    private RouletteResultModel model;
    private RouletteResultView view;

    public RouletteResultPresenter(RouletteResultModel rouletteResultModel, RouletteResultView rouletteResultView)
    {
        this.model = rouletteResultModel;
        this.view = rouletteResultView;
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

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public void ShowResult(RouletteSlotValue rouletteSlotValue)
    {
        model.ShowResult(rouletteSlotValue);
    }

    #endregion
}
