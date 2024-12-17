using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPresenter
{
    private CardModel model;
    private CardView view;

    public CardPresenter(CardModel model, CardView view)
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
        view.OnOpenCard += model.AddNominal;

        model.OnSpawnOpenCard += view.SpawnOpenCard;
        model.OnSpawnCloseCard += view.SpawnCloseCard;
        model.OnDestroysCards += view.DestroyCards;
        model.OnOpenCard += view.OpenCard;
    }

    private void DeactivateEvents()
    {
        view.OnOpenCard -= model.AddNominal;

        model.OnSpawnOpenCard -= view.SpawnOpenCard;
        model.OnSpawnCloseCard -= view.SpawnCloseCard;
        model.OnDestroysCards -= view.DestroyCards;
        model.OnOpenCard -= view.OpenCard;
    }

    #region Input

    public event Action<int> OnAddNominal
    {
        add { model.OnAddNominal += value; }
        remove { model.OnAddNominal -= value; }
    }

    public event Action OnClearNominal
    {
        add { model.OnClearNominal += value; }
        remove { model.OnClearNominal -= value; }
    }

    public void SpawnCards(bool[] isOpens, Action OnComplete = null)
    {
        model.SpawnCard(isOpens, OnComplete);
    }

    public void ClearCards()
    {
        model.ClearCards();
    }

    public void OpenCard()
    {
        model.OpenCard();
    }

    #endregion
}
