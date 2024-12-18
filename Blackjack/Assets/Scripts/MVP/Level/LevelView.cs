using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelView : View
{
    [SerializeField] private List<LevelVisual> levelVisuals = new List<LevelVisual>();

    public void Initialize()
    {
        for (int i = 0; i < levelVisuals.Count; i++)
        {
            levelVisuals[i].OnClickToChooseLevel += HandlerClickToChooseLevel;
            levelVisuals[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < levelVisuals.Count; i++)
        {
            levelVisuals[i].OnClickToChooseLevel -= HandlerClickToChooseLevel;
            levelVisuals[i].Dispose();
        }
    }

    public void SelectLevel(int id)
    {
        levelVisuals.FirstOrDefault(data => data.ID == id).SelectLevel();
    }

    public void UnselectLevel(int id)
    {
        levelVisuals.FirstOrDefault(data => data.ID == id).DeselectLevel();
    }

    #region Input

    public event Action<int> OnChooseLevel;

    private void HandlerClickToChooseLevel(int id)
    {
        OnChooseLevel?.Invoke(id);
    }

    #endregion
}
