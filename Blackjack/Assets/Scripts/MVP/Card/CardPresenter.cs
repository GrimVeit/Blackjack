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
    }

    private void DeactivateEvents()
    {
        view.OnOpenCard -= model.AddNominal;

        model.OnSpawnOpenCard -= view.SpawnOpenCard;
        model.OnSpawnCloseCard -= view.SpawnCloseCard;
        model.OnDestroysCards -= view.DestroyCards;
    }

    #region Input

    public void SpawnCards(bool[] isOpens, Action OnComplete = null)
    {
        model.SpawnCard(isOpens, OnComplete);
    }

    #endregion
}
