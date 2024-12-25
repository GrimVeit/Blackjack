using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoModel
{
    public event Action<string, float, int, int> OnGetLevelDescription;

    public void SetLevel(string levelName, float minStack, int betMin, int betMax)
    {
        OnGetLevelDescription?.Invoke(levelName, minStack, betMin, betMax);
    }
}
