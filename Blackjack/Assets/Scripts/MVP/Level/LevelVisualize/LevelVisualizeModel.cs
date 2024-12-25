using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVisualizeModel
{
    public event Action<Level, bool> OnSetLevel;

    private IMoneyProvider moneyProvider;

    public LevelVisualizeModel(IMoneyProvider moneyProvider)
    {
        this.moneyProvider = moneyProvider;
    }

    public void SetLevel(Level level)
    {
        if (moneyProvider.CanAfford(level.MinMoney))
        {
            OnSetLevel?.Invoke(level, true);
        }
        else
        {
            OnSetLevel?.Invoke(level, false);
        }
    }
}
