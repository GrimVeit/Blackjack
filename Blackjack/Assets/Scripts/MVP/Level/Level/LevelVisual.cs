using DG.Tweening;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelVisual
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Button buttonChooseLevel;
    [SerializeField] private Image imageLevel;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteInactive;

    private Vector3 vectorNormalPosition;
    private Vector3 vectorLeftPosition;

    private Vector3 vectorNormalRotation = Vector3.zero;
    private Vector3 vectorNotNormalRotation = new Vector3(90, 0, 0);

    private Tween tweenMoveActivate;
    private Tween tweenMoveInactivate;

    private Tween tweenRotateActivate;
    private Tween tweenRotateInactivate;

    public void Initialize()
    {
        vectorNormalPosition = imageLevel.transform.localPosition;
        vectorLeftPosition = vectorNormalPosition - new Vector3(30, 0, 0);

        buttonChooseLevel.onClick.AddListener(HandleClickToChooseLevelButton);
    }

    public void Dispose()
    {
        buttonChooseLevel.onClick.RemoveListener(HandleClickToChooseLevelButton);
    }

    public void SelectLevel()
    {
        tweenMoveActivate?.Kill();
        tweenMoveInactivate?.Kill();
        tweenRotateActivate?.Kill();
        tweenRotateInactivate?.Kill();

        tweenRotateActivate = InactiveToActive_Tween();
        tweenRotateActivate.Play();
    }

    public void DeselectLevel()
    {
        tweenMoveActivate?.Kill();
        tweenMoveInactivate?.Kill();
        tweenRotateActivate?.Kill();
        tweenRotateInactivate?.Kill();

        tweenMoveActivate = imageLevel.transform.DOLocalMove(vectorNormalPosition, 0.2f);
        tweenMoveActivate.Play();

        tweenRotateActivate = imageLevel.transform.DORotate(vectorNormalRotation, 0.2f);
        tweenRotateActivate.Play();

        imageLevel.sprite = spriteInactive;
    }

    private Tween InactiveToActive_Tween()
    {
        Sequence openSequence = DOTween.Sequence();

        tweenMoveActivate = imageLevel.transform.DOLocalMove(vectorLeftPosition, 0.2f);

        openSequence.Append(imageLevel.transform.DORotate(vectorNotNormalRotation, 0.2f).OnComplete(() =>
        {
            imageLevel.sprite = spriteActive;

            tweenMoveInactivate = imageLevel.transform.DOLocalMove(vectorNormalPosition, 0.2f);
        }));

        openSequence.Append(imageLevel.transform.DORotate(vectorNormalRotation, 0.2f).OnComplete(() =>
        {
            
        }));

        return openSequence;
    }

    private Tween ActiveToInactive_Tween()
    {
        Sequence openSequence = DOTween.Sequence();

        openSequence.Append(imageLevel.transform.DORotate(vectorNotNormalRotation, 0.2f).OnComplete(() =>
        {
            imageLevel.sprite = spriteInactive;
        }));

        openSequence.Append(imageLevel.transform.DORotate(vectorNormalRotation, 0.2f).OnComplete(() =>
        {

        }));

        return openSequence;
    }

    #region Input

    public event Action<int> OnClickToChooseLevel;

    private void HandleClickToChooseLevelButton()
    {
        Debug.Log("Click to " +  id);

        OnClickToChooseLevel?.Invoke(id);
    }

    #endregion
}
