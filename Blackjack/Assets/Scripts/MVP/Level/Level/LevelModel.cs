using System;
using UnityEngine;

public class LevelModel
{
    public event Action<int> OnSelectLevel;
    public event Action<int> OnUnselectLevel;

    public event Action<int> OnSelectLevel_InputAction;

    public void InputSelectLevel(int level)
    {
        OnSelectLevel_InputAction?.Invoke(level);
    }

    public void SelectLevel(Level level)
    {
        OnSelectLevel?.Invoke(level.IDLevel);
    }

    public void UnselectLevel(Level level)
    {
        OnUnselectLevel?.Invoke(level.IDLevel);
    }
}
