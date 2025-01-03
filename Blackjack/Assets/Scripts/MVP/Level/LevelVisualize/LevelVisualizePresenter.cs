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
        model.OnSetLevel += view.SetLevel;
    }

    private void DeactivateEvents()
    {
        model.OnSetLevel -= view.SetLevel;
    }

    #region Input

    public void SetLevel(Level level)
    {
        model.SetLevel(level);
    }

    #endregion
}
