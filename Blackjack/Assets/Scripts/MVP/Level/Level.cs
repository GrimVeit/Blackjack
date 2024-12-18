using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField] private int idLevel;
    [SerializeField] private string nameLevel;
    [SerializeField] private Sprite spriteLevelDescription;
    [SerializeField] private Sprite spriteLevelBackground;
    [SerializeField] private float minMoney;
    [SerializeField] private float minBet;
    [SerializeField] private float maxBet;

    public int IDLevel => idLevel;
    public string NameLevel => nameLevel;
    public Sprite SpriteDescriptionLevel => spriteLevelDescription;
    public Sprite SpriteBackgroundLevel => spriteLevelBackground;
    public float MinMoney => minMoney;
    public float MinBet => minBet;
    public float MaxBet => maxBet;
}
