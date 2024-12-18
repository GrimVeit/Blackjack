using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVisualizePresenter
{
    private LevelVisualizeModel model;
    private LevelVisualizeView view;

    public LevelVisualizePresenter(LevelVisualizeModel model, LevelVisualizeView view)
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
        model.OnSetData += view.SetData;
    }

    private void DeactivateEvents()
    {
        model.OnSetData -= view.SetData;
    }

    #region Input

    public void SetData(Level level)
    {
        model.SetData(level);
    }

    #endregion
}
