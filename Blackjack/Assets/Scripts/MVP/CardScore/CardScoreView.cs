using TMPro;
using UnityEngine;

public class CardScoreView : View
{
    [SerializeField] private TextMeshProUGUI textScorePlayer;
    [SerializeField] private TextMeshProUGUI textScoreDealer;

    public void DisplayScore_Player(int score)
    {
        textScorePlayer.text = score.ToString();
    }

    public void DisplayScore_Dealer(int score)
    {
        textScoreDealer.text = score.ToString();
    }
}
