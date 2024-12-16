using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardView : View, IIdentify
{
    public string GetID() => id;

    public event Action<int> OnOpenCard;

    [SerializeField] private string id;
    [SerializeField] private List<CardZone> zones = new List<CardZone>();
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform transformSpawn;
    [SerializeField] private Transform transformDestroy;


    private List<Card> cards = new List<Card>();

    public void SpawnOpenCard(CardData cardData)
    {
        Debug.Log("Spawn open, nominal - " + cardData.Nominal);

        var transform = zones[cards.Count].transformZone;
        var card = Instantiate(cardPrefab, transform);
        card.SetData(cardData);
        card.transform.SetPositionAndRotation(transformSpawn.position, cardPrefab.transform.rotation);
        card.MoveTo(transform.position, 0.2f, card.OpenCard);
        card.OnOpenCard += HandleOpenCard;

        cards.Add(card);
    }

    public void SpawnCloseCard(CardData cardData)
    {
        Debug.Log("Spawn close, nominal - " + cardData.Nominal);

        var transform = zones[cards.Count].transformZone;
        var card = Instantiate(cardPrefab, transform);
        card.SetData(cardData);
        card.transform.SetPositionAndRotation(transformSpawn.position, cardPrefab.transform.rotation);
        card.MoveTo(transform.position, 0.2f);
        card.OnOpenCard += HandleOpenCard;

        cards.Add(card);
    }

    public void OpenCard()
    {
        cards.FirstOrDefault(data => data.IsOpen).OpenCard();
    }

    public void DestroyCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].MoveTo(transformDestroy.position, 0.2f, cards[i].DestroyCard);
        }

        cards.Clear();
    }

    private void HandleOpenCard(int nominal)
    {
        OnOpenCard?.Invoke(nominal);
    }
}
