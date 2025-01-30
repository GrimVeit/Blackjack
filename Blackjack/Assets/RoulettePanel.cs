using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoulettePanel : MovePanel
{
    [SerializeField] private List<ScaleEffectCombination> scaleEffects = new List<ScaleEffectCombination>();
    [SerializeField] private Button buttonExit;

    public override void Initialize()
    {
        buttonExit.onClick.AddListener(() => OnClickExit?.Invoke());
    }

    public override void Dispose()
    {
        buttonExit.onClick.RemoveListener(() => OnClickExit?.Invoke());
    }

    public override void ActivatePanel()
    {
        scaleEffects.ForEach(effect => effect.ActivateEffect());

        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        scaleEffects.ForEach(effect => effect.DeactivateEffect());

        base.DeactivatePanel();
    }

    #region Input

    public event Action OnClickExit;

    #endregion
}
