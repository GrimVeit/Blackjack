using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressPresenter
{
    private GameProgressModel model;

    public GameProgressPresenter(GameProgressModel model)
    {
        this.model = model;
    }

    public void Initialize()
    {
        model.Initialize();
    }

    public void Dispose()
    {
        model.Dispose();
    }

    #region Input

    public event Action<Level> OnSelectLevel
    {
        add { model.OnSelectLevel += value; }
        remove { model.OnSelectLevel -= value; }
    }

    public event Action<Level> OnUnselectLevel
    {
        add { model.OnUnselectLevel += value; }
        remove { model.OnUnselectLevel -= value; }
    }

    public void ChooseLevel(int id)
    {
        model.ChooseLevel(id);
    }

    #endregion
}
