using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVisualizeModel
{
    public event Action<Level, bool> OnSetData;

    private IMoneyProvider moneyProvider;

    public LevelVisualizeModel(IMoneyProvider moneyProvider)
    {
        this.moneyProvider = moneyProvider;
    }

    public void SetData(Level level)
    {
        if (moneyProvider.CanAfford(level.MinMoney))
        {
            OnSetData?.Invoke(level, true);
        }
        else
        {
            OnSetData?.Invoke(level, false);
        }
    }
}
