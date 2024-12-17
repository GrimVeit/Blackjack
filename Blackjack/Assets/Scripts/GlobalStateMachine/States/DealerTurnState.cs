using UnityEngine;

public class DealerTurnState : IGlobalState
{
    private IGlobalMachineControl machineControl;

    private CardScorePresenter cardScorePresenter;
    private CardPresenter cardPresenter_Dealer;


    public DealerTurnState(IGlobalMachineControl machineControl, CardScorePresenter cardScorePresenter, CardPresenter cardPresenter_Dealer)
    {
        this.machineControl = machineControl;
        this.cardScorePresenter = cardScorePresenter;
        this.cardPresenter_Dealer = cardPresenter_Dealer;
    }

    public void EnterState()
    {
        cardPresenter_Dealer.OnAddNominal += OnOpenCard;

        cardPresenter_Dealer.OpenCard();
    }

    public void ExitState()
    {
        cardPresenter_Dealer.OnAddNominal -= OnOpenCard;
    }

    private void OnOpenCard(int nominal)
    {
        cardScorePresenter.AddScore_Dealer(nominal);

        if(cardScorePresenter.ScoreDealer < 17)
        {
            cardPresenter_Dealer.SpawnCards(new bool[] { true });
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
