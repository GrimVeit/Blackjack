using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfoView : View
{
    [SerializeField] private TextMeshProUGUI textNameGame;
    [SerializeField] private TextMeshProUGUI textMinStack;
    [SerializeField] private TextMeshProUGUI textMinBet;
    [SerializeField] private TextMeshProUGUI textMaxBet;

    public void SetLevelDescription(string levelName, float minStack, int betMin, int betMax)
    {
        textNameGame.text = levelName;

        textMinStack.text = $"Min stack: {minStack}";
        textMinBet.text = $"Bet min: {betMin}";
        textMaxBet.text = $"Bet max: {betMax}";
    }
}
