using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public bool IsOpen { get; private set; }
    public event Action<int> OnOpenCard;

    [SerializeField] private Transform transformCard;
    [SerializeField] private Transform transformFront;
    [SerializeField] private Transform transformBack;
    [SerializeField] private Image imageCard;

    private Vector3 vectorFront = new Vector3 (0, 0, 0);
    private Vector3 vectorBack = new Vector3(0, 90, 0);

    private bool isOpen = false;

    private CardData currentCardData;

    public void SetData(CardData cardData)
    {
        currentCardData = cardData;
        imageCard.sprite = currentCardData.Sprite;
    }

    public void MoveTo(Vector3 vector, float time, Action OnComplete = null)
    {
        transformCard.DOMove(vector, time).OnComplete(() =>
        {
            OnComplete?.Invoke();
        });
    }

    public void OpenCard()
    {
        OpenCard_Tween().Play();
    }

    public void CloseCard()
    {
        CloseCard_Tween().Play();
    }

    public void DestroyCard()
    {
        Destroy(transformCard.gameObject);
    }

    private Tween OpenCard_Tween()
    {
        Sequence openSequence = DOTween.Sequence();

        openSequence.Append(transformBack.DORotate(vectorBack, 0.2f).OnComplete(() =>
        {
            transformFront.gameObject.SetActive(true);
            transformBack.gameObject.SetActive(false);
        }));

        openSequence.Append(transformFront.DORotate(vectorFront, 0.2f).OnComplete(() =>
        {
            isOpen = true;
            OnOpenCard?.Invoke(currentCardData.Nominal);
        }));

        return openSequence;
    }

    private Tween CloseCard_Tween()
    {
        Sequence closeSequence = DOTween.Sequence();

        closeSequence.Append(transformFront.DORotate(vectorBack, 0.2f).OnComplete(() =>
        {
            transformFront.gameObject.SetActive(false);
            transformBack.gameObject.SetActive(true);
        }));

        closeSequence.Append(transformBack.DORotate(vectorFront, 0.2f).OnComplete(() =>
        {
            isOpen = false;
        }));

        return closeSequence;
    }
}
