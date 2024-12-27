using DG.Tweening;
using UnityEngine;

public class ScaleEffect_Yoyo : ScaleEffect
{
    [SerializeField] private Vector3 scaleMax;
    [SerializeField] private Vector3 scaleMin;
    [SerializeField] private float duration;
    [SerializeField] private bool isAwake;
    [SerializeField] private Transform scaleElement;

    private Tween tweenYoyo;

    public override void Initialize()
    {
        scaleElement.localScale = scaleMin;

        if (isAwake)
            ActivateEffect();
    }

    public override void Dispose()
    {
        tweenYoyo?.Kill();
    }

    public override void ActivateEffect()
    {
        tweenYoyo?.Kill();

        tweenYoyo = scaleElement.DOScale(scaleMax, duration).SetLoops(-1, LoopType.Yoyo);
    }
}
