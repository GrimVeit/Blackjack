using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPresenter
{
    private LevelModel model;
    private LevelView view;

    public LevelPresenter(LevelModel model, LevelView view)
    {
        this.model = model;
        this.view = view;
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
        view.OnChooseLevel += model.SelectLevel;

        model.OnSelectLevel += view.SelectLevel;
        model.OnUnselectLevel += view.UnselectLevel;
    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public event Action<Level> OnChooseLevel
    {
        add { model.OnChooseLevel += value; }
        remove { model.OnChooseLevel -= value; }
    }

    #endregion
}
