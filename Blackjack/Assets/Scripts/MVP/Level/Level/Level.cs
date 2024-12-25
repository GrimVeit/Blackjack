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
    [SerializeField] private int minBet;
    [SerializeField] private int maxBet;
    [SerializeField] private List<int> betNominals;

    public int IDLevel => idLevel;
    public string NameLevel => nameLevel;
    public Sprite SpriteDescriptionLevel => spriteLevelDescription;
    public Sprite SpriteBackgroundLevel => spriteLevelBackground;
    public float MinMoney => minMoney;
    public int MinBet => minBet;
    public int MaxBet => maxBet;
    public List<int> BetNominals => betNominals;
}
