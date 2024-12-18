using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels")]
public class Levels : ScriptableObject
{
    public List<Level> levels = new List<Level>();

    public Level GetLevelByID(int levelID)
    {
        return levels.FirstOrDefault(data => data.IDLevel == levelID);
    }
}
