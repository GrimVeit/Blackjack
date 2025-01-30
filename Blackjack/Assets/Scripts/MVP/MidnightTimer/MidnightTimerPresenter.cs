using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidnightTimerPresenter
{
    private MidnightTimerModel model;
    private MidnightTimerView view;

    public MidnightTimerPresenter(MidnightTimerModel model, MidnightTimerView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        model.Dispose();
    }

    private void ActivateEvents()
    {
        model.OnItterationCountdown += view.SetTimer;
    }

    private void DeactivateEvents()
    {
        model.OnItterationCountdown -= view.SetTimer;
    }
}
