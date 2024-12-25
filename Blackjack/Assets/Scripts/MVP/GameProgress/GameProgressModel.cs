using System;
using UnityEngine;

public class GameProgressModel
{
    public event Action<Level> OnSelectLevel;
    public event Action<Level> OnUnselectLevel;

    private Levels levels;

    private Level currentLevel;

    public GameProgressModel(Levels levels)
    {
        this.levels = levels;
    }

    public void Initialize()
    {
        currentLevel = levels.GetLevelByID(PlayerPrefs.GetInt(PlayerPrefsKeys.LEVEL, 0));
        OnSelectLevel?.Invoke(currentLevel);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.LEVEL, currentLevel.IDLevel);
    }

    public void ChooseLevel(int id)
    {
        if (currentLevel?.IDLevel == id)
            return;

        OnUnselectLevel?.Invoke(currentLevel);

        currentLevel = levels.GetLevelByID(id);
        OnSelectLevel?.Invoke(currentLevel);

    }
}
