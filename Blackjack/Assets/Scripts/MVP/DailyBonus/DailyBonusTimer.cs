using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyBonusTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimer;

    public void SetTransform(Transform transformParent)
    {
        transform.SetParent(transformParent.parent);
        transform.localPosition = Vector3.zero;
    }

    public void SetTime(string time)
    {
        textTimer.text = time;
    }

    public void ActivateTimer()
    {
        transform.gameObject.SetActive(true);
    }

    public void DeactivateTimer()
    {
        transform.gameObject.SetActive(false);
    }
}
