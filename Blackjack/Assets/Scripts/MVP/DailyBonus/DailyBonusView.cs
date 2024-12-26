using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusView : View
{
    [SerializeField] private List<DailyBonus> dailyBonuses = new List<DailyBonus>();
    [SerializeField] private Button buttonClaim;

    public void Initialize()
    {
        buttonClaim.onClick.AddListener(HandleClickToClaimButton);
    }

    public void Dispose()
    {
        buttonClaim.onClick.RemoveListener(HandleClickToClaimButton);
    }

    public void ActivateClaimButton()
    {
        buttonClaim.gameObject.SetActive(true);
    }

    public void DeactivateClaimButton()
    {
        buttonClaim.gameObject.SetActive(false);
    }


    public void CurrentClaim(int day)
    {
        dailyBonuses.FirstOrDefault(data => data.Day == day).CurrentClaim();
    }

    public void AlreadyClaim(int day)
    {
        dailyBonuses.FirstOrDefault(data => data.Day == day).AlreadyClaim();
    }

    public void CloseClaim(int day)
    {
        dailyBonuses.FirstOrDefault(data => data.Day == day).CloseClaim();
    }

    #region Input

    public event Action OnClickToClaimButton;

    private void HandleClickToClaimButton()
    {
        OnClickToClaimButton?.Invoke();
    }

    #endregion
}
