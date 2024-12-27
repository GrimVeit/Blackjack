using DG.Tweening;
using UnityEngine;

public class ScaleEffect_Fade : ScaleEffect
{
    [SerializeField] private float duration;
    [SerializeField] private Transform scaleElement;

    private Tween tweenFade;

    private Vector3 scaleNormal;

    public override void Initialize()
    {
        scaleNormal = scaleElement.localScale;
        scaleElement.localScale = Vector3.zero;
    }

    public override void Dispose()
    {
        tweenFade?.Kill();
    }

    public override void ActivateEffect()
    {
        tweenFade?.Kill();

        tweenFade = scaleElement.DOScale(scaleNormal, duration);
    }

    public override void DeactivateEffect()
    {
        tweenFade?.Kill();

        tweenFade = scaleElement.DOScale(Vector3.zero, duration);
    }
}
