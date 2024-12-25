using System;

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

        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnChooseLevel += model.InputSelectLevel;

        model.OnSelectLevel += view.SelectLevel;
        model.OnUnselectLevel += view.UnselectLevel;
    }

    private void DeactivateEvents()
    {
        view.OnChooseLevel -= model.InputSelectLevel;

        model.OnSelectLevel -= view.SelectLevel;
        model.OnUnselectLevel -= view.UnselectLevel;
    }

    #region Input

    public event Action<int> OnChooseLevel
    {
        add { model.OnSelectLevel_InputAction += value; }
        remove { model.OnSelectLevel_InputAction -= value; }
    }

    public void SelectLevel(Level level)
    {
        model.SelectLevel(level);
    }

    public void UnselectLevel(Level level)
    {
        model.UnselectLevel(level);
    }

    #endregion
}
