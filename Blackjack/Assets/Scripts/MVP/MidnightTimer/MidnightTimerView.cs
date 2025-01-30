using TMPro;
using UnityEngine;

public class MidnightTimerView : View
{
    [SerializeField] private TextMeshProUGUI textTime;

    public void SetTimer(string timer)
    {
        textTime.text = timer;
    }
}
