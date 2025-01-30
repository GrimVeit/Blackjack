using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidnightTimerModel
{
    public event Action<string> OnItterationCountdown;
    public event Action OnResetCountdown;

    private DateTime nextMidnightUtc;

    private TimeSpan timeRemaining;

    private IEnumerator timerCoroutine;

    public void Initialize()
    {
        Debug.Log(DateTime.UtcNow);

        LoadNextMidnightTimer();

        if (DateTime.UtcNow >= nextMidnightUtc)
        {
            nextMidnightUtc = GetNextMidnightDatetime();
            OnResetCountdown?.Invoke();
        }

        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        timerCoroutine = TimerCoroutine();
        Coroutines.Start(timerCoroutine);
    }

    public void Dispose()
    {
        if(timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        SaveNextMidnightTimer();
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            timeRemaining = nextMidnightUtc - DateTime.UtcNow;

            //Debug.Log(nextMidnightUtc);

            OnItterationCountdown?.Invoke(string.Format(
                "{0:D2}:{1:D2}:{2:D2}", 
                timeRemaining.Hours, 
                timeRemaining.Minutes, 
                timeRemaining.Seconds));

            if(timeRemaining.TotalSeconds <= 0)
            {
                nextMidnightUtc = GetNextMidnightDatetime();
                OnResetCountdown?.Invoke();
            }

            yield return new WaitForSeconds(1);
        }
    }

    private DateTime GetNextMidnightDatetime()
    {
        DateTime currentDateTime = DateTime.UtcNow;
        DateTime midnightUtc = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, 0, 0, 0, DateTimeKind.Utc);

        midnightUtc = midnightUtc.AddDays(1);

        return midnightUtc;
    }

    private void LoadNextMidnightTimer()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.NEXT_MIDNIGHT_DATETIME))
        {
            nextMidnightUtc = DateTime.Parse(PlayerPrefs.GetString(PlayerPrefsKeys.NEXT_MIDNIGHT_DATETIME));
        }
        else
        {
            nextMidnightUtc = DateTime.MinValue;
        }
    }

    private void SaveNextMidnightTimer()
    {
        PlayerPrefs.SetString(PlayerPrefsKeys.NEXT_MIDNIGHT_DATETIME, nextMidnightUtc.ToString());
    }
}
