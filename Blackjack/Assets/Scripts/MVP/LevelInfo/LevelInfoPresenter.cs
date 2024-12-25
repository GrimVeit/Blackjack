using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoPresenter
{
    private LevelInfoModel model;
    private LevelInfoView view;

    public LevelInfoPresenter(LevelInfoModel model, LevelInfoView view)
    {
        this.model = model;
        this.view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        model.OnGetLevelDescription += view.SetLevelDescription;
    }

    private void DeactivateEvents()
    {
        model.OnGetLevelDescription -= view.SetLevelDescription;
    }

    #region Input

    public void SetLevel(Level level)
    {
        model.SetLevel(level.NameLevel, level.MinMoney, level.MinBet, level.MaxBet);
    }

    #endregion
}
