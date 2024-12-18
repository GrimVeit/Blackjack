using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public List<Level> levels = new List<Level>();

    public Level GetLevelByID(int levelID)
    {
        return levels.FirstOrDefault(data => data.IDLevel == levelID);
    }
}
