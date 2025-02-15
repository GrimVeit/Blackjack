using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DailyBonusModel
{
    public event Action<int> OnActivateTimer;
    public event Action OnDeactivateTimer;
    public event Action<string> OnCountdownTimer;

    public event Action OnActivatedClaimButton;
    public event Action OnDeactivatedClaimButton;

    public event Action<int> OnCurrentClaimBonus;
    public event Action<int> OnAlreadyClaimBonus;
    public event Action<int> OnNotClaimBonus;

    private TimeSpan ostatokForGetBonusDateTime => nextClaimStart - currentDateTime;
    private TimeSpan ostatokForFallenBonusDateTime => nextClaimEnd - currentDateTime;

    private List<DailyBonusData> bonusesDatas = new List<DailyBonusData>();

    private DailyBonusData currentDailyBonus;

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "DailyBonus.json");

    private DateTime currentDateTime => DateTime.UtcNow;
    private DateTime nextClaimStart => DateTime.Parse(currentDailyBonus.NextClaimStart);
    private DateTime nextClaimEnd => DateTime.Parse(currentDailyBonus.NextClaimEnd);

    private IEnumerator fallenBonusCoroutine;
    private IEnumerator getBonusCoroutine;

    private IMoneyProvider moneyProvider;

    public DailyBonusModel(IMoneyProvider moneyProvider)
    {
        this.moneyProvider = moneyProvider;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            Bonuses bonuses = JsonUtility.FromJson<Bonuses>(loadedJson);

            Debug.Log("Success");

            this.bonusesDatas = bonuses.DailyBonuses.ToList();
        }
        else
        {
            bonusesDatas = new List<DailyBonusData>()
            {
                new DailyBonusData(0, 100, false, DateTime.UtcNow, DateTime.MaxValue),
                new DailyBonusData(1, 150, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(2, 250, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(3, 500, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(4, 1000, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(5, 1500, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(6, 2500, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(7, 5000, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(8, 10000, false, DateTime.UtcNow, DateTime.UtcNow),
                new DailyBonusData(9, 25000, false, DateTime.UtcNow, DateTime.UtcNow)
            };
        }

        for (int i = 0; i < bonusesDatas.Count; i++)
        {
            if (bonusesDatas[i].IsClaimed)
            {
                OnAlreadyClaimBonus?.Invoke(i);
            }
            else
            {
                OnNotClaimBonus?.Invoke(i);
            }
        }

        CheckGetBonus();
    }

    public void Dispose()
    {
        if (fallenBonusCoroutine != null)
            Coroutines.Stop(fallenBonusCoroutine);

        if(getBonusCoroutine != null)
            Coroutines.Stop(getBonusCoroutine);

        string json = JsonUtility.ToJson(new Bonuses(bonusesDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    private void CheckGetBonus()
    {
        currentDailyBonus = GetFirstNotClaimedBonus();

        if (currentDateTime > nextClaimEnd)
        {
            OnDeactivatedClaimButton?.Invoke();
            RestartAll();
        }

        if (currentDateTime < nextClaimStart)
        {
            OnDeactivatedClaimButton?.Invoke();

            getBonusCoroutine = CountdownActivateBonus_Coroutine();
            Coroutines.Start(getBonusCoroutine);
            return;
        }

        if(currentDateTime >= nextClaimStart)
        {
            OnCurrentClaimBonus?.Invoke(currentDailyBonus.Day);
            OnActivatedClaimButton?.Invoke();

            fallenBonusCoroutine = CountdownDeactivateBonus_Coroutine();
            Coroutines.Start(fallenBonusCoroutine);
        }
    }

    public void ClaimBonus()
    {
        OnDeactivatedClaimButton?.Invoke();

        currentDailyBonus.IsClaimed = true;
        moneyProvider.SendMoney(currentDailyBonus.Coins);
        Debug.Log(currentDailyBonus.Coins.ToString());
        OnAlreadyClaimBonus?.Invoke(currentDailyBonus.Day);

        if(fallenBonusCoroutine != null)
           Coroutines.Stop(fallenBonusCoroutine);

        if(currentDailyBonus.Day + 1 >= bonusesDatas.Count)
        {
            OnDeactivatedClaimButton?.Invoke();
            RestartAll();
        }
        else
        {
            currentDailyBonus = bonusesDatas[currentDailyBonus.Day + 1];
            currentDailyBonus.NextClaimStart = (currentDateTime + TimeSpan.FromDays(1)).ToString();
            currentDailyBonus.NextClaimEnd = (currentDateTime + TimeSpan.FromDays(2)).ToString();
        }

        CheckGetBonus();
    }

    private void RestartAll()
    {
        for (int i = 0; i < bonusesDatas.Count; i++)
        {
            bonusesDatas[i].IsClaimed = false;
            OnNotClaimBonus?.Invoke(bonusesDatas[i].Day);
        }

        currentDailyBonus = GetFirstNotClaimedBonus();
        currentDailyBonus.NextClaimStart = (currentDateTime + TimeSpan.FromDays(1)).ToString();
        currentDailyBonus.NextClaimEnd = (currentDateTime + TimeSpan.FromDays(2)).ToString();
    }

    private IEnumerator CountdownActivateBonus_Coroutine()
    {
        OnActivateTimer?.Invoke(currentDailyBonus.Day);

        TimeSpan timeRemaining = ostatokForGetBonusDateTime;

        while (true)
        {
            if (timeRemaining.TotalSeconds <= 1)
            {
                OnDeactivateTimer?.Invoke();

                OnCurrentClaimBonus?.Invoke(currentDailyBonus.Day);
                OnActivatedClaimButton?.Invoke();

                fallenBonusCoroutine = CountdownDeactivateBonus_Coroutine();
                Coroutines.Start(fallenBonusCoroutine);
                break;
            }

            //Debug.Log(string.Format("TIME FOR GET BONUS: " + "{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));
            OnCountdownTimer?.Invoke(string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));

            timeRemaining -= TimeSpan.FromSeconds(1);

            yield return new WaitForSeconds(1);
        }

        OnCountdownTimer?.Invoke("");
    }

    private IEnumerator CountdownDeactivateBonus_Coroutine()
    {
        TimeSpan timeRemaining = ostatokForFallenBonusDateTime;
        Debug.Log(timeRemaining.Seconds);
        while (true)
        {
            if (timeRemaining.TotalSeconds <= 1)
            {
                OnDeactivatedClaimButton?.Invoke();
                RestartAll();
                CheckGetBonus();
                break;
            }

            //Debug.Log(string.Format("TIME FOR FALLEN BONUS: " + "{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));
            OnCountdownTimer?.Invoke(string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));

            timeRemaining -= TimeSpan.FromSeconds(1);

            yield return new WaitForSeconds(1);
        }

        OnCountdownTimer?.Invoke("");
    }


    private DailyBonusData GetFirstNotClaimedBonus()
    {
        return bonusesDatas.FirstOrDefault(data => !data.IsClaimed);
    }
}

[Serializable]
public class Bonuses
{
    public DailyBonusData[] DailyBonuses;

    public Bonuses(DailyBonusData[] dailyBonuses)
    {
        DailyBonuses = dailyBonuses;
    }
}

[Serializable]
public class DailyBonusData
{
    public int Day;
    public int Coins;
    public bool IsClaimed;
    public string NextClaimStart;
    public string NextClaimEnd;

    public DailyBonusData(int day, int coins, bool isClaimed, DateTime nextClaimStart, DateTime nextClaimEnd)
    {
        Day = day;
        Coins = coins;
        IsClaimed = isClaimed;
        NextClaimStart = nextClaimStart.ToString();
        NextClaimEnd = nextClaimEnd.ToString();
    }
}
