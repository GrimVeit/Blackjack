using System;

public class CardScorePresenter
{
    private CardScoreModel model;
    private CardScoreView view;

    public CardScorePresenter(CardScoreModel model, CardScoreView view)
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
        model.OnChangeScore_Player += view.DisplayScore_Player;
        model.OnChangeScore_Dealer += view.DisplayScore_Dealer;
    }

    private void DeactivateEvents()
    {
        model.OnChangeScore_Player -= view.DisplayScore_Player;
        model.OnChangeScore_Dealer -= view.DisplayScore_Dealer;
    }

    #region Input

    public event Action OnWin
    {
        add { model.OnWin += value; }
        remove { model.OnWin -= value; }
    }

    public event Action OnLose
    {
        add { model.OnLose += value; }
        remove { model.OnLose -= value; }
    }

    public void AddScore_Player(int score)
    {
        model.AddScore_Player(score);
    }

    public void AddScore_Dealer(int score)
    {
        model.AddScore_Dealer(score);
    }

    public void ClearScore_Player()
    {
        model.ClearScore_Player();
    }

    public void ClearScore_Dealer()
    {
        model.ClearScore_Dealer();
    }

    public void CalculateResult()
    {
        model.CalculateResult();
    }

    #endregion
}
