using System;
using UnityEngine;

public class LevelModel
{
    public event Action<Level> OnChooseLevel;
    public event Action<int> OnSelectLevel;
    public event Action<int> OnUnselectLevel;

    private Levels levels;

    private Level currentLevel;

    public LevelModel(Levels levels)
    {
        this.levels = levels;
    }

    public void Initialize()
    {
        currentLevel = levels.GetLevelByID(PlayerPrefs.GetInt(PlayerPrefsKeys.LEVEL, 0));
        OnChooseLevel?.Invoke(currentLevel);
        OnSelectLevel?.Invoke(currentLevel.IDLevel);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.LEVEL, currentLevel.IDLevel);
    }

    public void SelectLevel(int id)
    {
        OnUnselectLevel?.Invoke(currentLevel.IDLevel);

        currentLevel = levels.GetLevelByID(id);
        OnSelectLevel?.Invoke(currentLevel.IDLevel);
        OnChooseLevel?.Invoke(currentLevel);

    }
}
