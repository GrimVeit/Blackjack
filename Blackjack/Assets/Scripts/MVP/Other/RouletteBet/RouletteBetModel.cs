using System;
using UnityEngine;

public class RouletteBetModel
{
    public event Action OnChooseBet;

    public event Action OnSuccess;
    public event Action OnFailure;

    public event Action OnActivate;
    public event Action OnDeactivate;

    private ColorNumber colorBet;

    private bool isActivate;

    public void Initialize()
    {
        isActivate = PlayerPrefs.GetInt(PlayerPrefsKeys.IS_ACTIVE_ROULETTE) == 1 ? true : false;

        if (isActivate)
        {
            OnActivate?.Invoke();
        }
        else
        {
            OnDeactivate?.Invoke();
        }
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.IS_ACTIVE_ROULETTE, isActivate == true ? 1 : 0);
    }

    public void SetBetBlack()
    {
        colorBet = ColorNumber.Black;

        OnChooseBet?.Invoke();
        OnDeactivate?.Invoke();
    }

    public void SetBetRed()
    {
        colorBet = ColorNumber.Red;

        OnChooseBet?.Invoke();
        OnDeactivate?.Invoke();
    }

    public void SetResult(ColorNumber color)
    {
        if(colorBet == color)
        {
            OnSuccess?.Invoke();
        }
        else
        {
            OnFailure?.Invoke();
        }
    }

    public void ActivateBet()
    {
        OnActivate?.Invoke();
    }
}
