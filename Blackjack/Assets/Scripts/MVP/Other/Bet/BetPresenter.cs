using System;

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
        view.OnClickToDoubleBetButton += model.DoubleBet;

        model.OnChangeBet += view.DisplayChangeBet;
        model.OnActivateGameButton += view.ActivateStartButton;
        model.OnDeactivateGameButton += view.DeactivateStartButton;
    }

    public void DeactivateEvents()
    {
        view.OnClickToMaxBetButton -= model.MaxBet;
        view.OnClickToDoubleBetButton -= model.DoubleBet;

        model.OnChangeBet -= view.DisplayChangeBet;
        model.OnActivateGameButton -= view.ActivateStartButton;
        model.OnDeactivateGameButton -= view.DeactivateStartButton;
    }

    #region Input

    public int CurrentBet => model.CurrentBet;

    public event Action<float> OnCountForMaxBet
    {
        add { model.OnCountForMaxBet += value; }
        remove { model.OnCountForMaxBet -= value; }
    }

    public void SetLevel(Level level)
    {
        model.SetMinMaxBet(level.MinBet, level.MaxBet);
    }

    public void AddBet(int bet)
    {
        model.AddBet(bet);
    }

    public void RemoveBet(int bet)
    {
        model.RemoveBet(bet);
    }

    public void ReturnBet()
    {
        model.ReturnBet();
    }

    public void ClearBet()
    {
        model.ClearBet();
    }

    public bool IsAvailable(int bet)
    {
        return model.IsAvailable(bet);
    }

    public void Activate()
    {
        model.Activate();
    }

    public void Deactivate()
    {
        model.Deactivate();
    }

    public void DisplayWin(int win)
    {
        view.DisplayWin(win);
    }

    #endregion
}

public interface IBetProvider
{
    bool IsAvailable(int bet);
}
