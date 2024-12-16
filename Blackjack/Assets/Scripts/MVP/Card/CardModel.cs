using System;
using System.Collections;
using UnityEngine;

public class CardModel
{
    public event Action<int> OnAddNominal;
    public event Action OnClearNominal;

    public event Action OnDestroysCards;
    public event Action<CardData> OnSpawnOpenCard;
    public event Action<CardData> OnSpawnCloseCard;

    private CardDatas cardDatas;

    public CardModel(CardDatas cardDatas)
    {
        this.cardDatas = cardDatas;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void AddNominal(int nominal)
    {
        OnAddNominal?.Invoke(nominal);
    }

    public void ClearCards()
    {
        OnClearNominal?.Invoke();
        OnDestroysCards?.Invoke();
    }

    public void SpawnCard(bool[] isOpens, Action OnDone = null)
    {
        Coroutines.Start(SpawnCard_Coroutine(isOpens, OnDone));
    }

    public IEnumerator SpawnCard_Coroutine(bool[] isOpens, Action OnDone)
    {
        for (int i = 0; i < isOpens.Length; i++)
        {
            if (isOpens[i] == true)
            {
                OnSpawnOpenCard?.Invoke(cardDatas.GetRandomData());
            }
            else
            {
                OnSpawnCloseCard?.Invoke(cardDatas.GetRandomData());
            }

            yield return new WaitForSeconds(0.4f);
        }

        OnDone?.Invoke();
    }
}
