using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card datas")]
public class CardDatas : ScriptableObject
{
    public List<CardData> Datas = new List<CardData>();

    public CardData GetRandomData()
    {
        return Datas[Random.Range(0, Datas.Count)];
    }
}

[System.Serializable]
public class CardData
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int nominal;

    public Sprite Sprite => sprite;
    public int Nominal => nominal;
}
