using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DailyBonusModel
{
    public event Action<string> OnCountdownTimer;

    public event Action OnActivatedClaimButton;
    public event Action OnDeactivatedClaimButton;

    public event Action<int> OnCurrentClaimBonus;
    public event Action<int> OnAlreadyClaimBonus;
    public event Action<int> OnNotClaimBonus;

    private TimeSpan ostatokDateTime;

    private List<DailyBonusData> bonusesDatas = new List<DailyBonusData>();

    private DailyBonusData currentDailyBonus;

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "DailyBonus.json");

    private DateTime currentDateTime => DateTime.UtcNow;
    private DateTime nextClaimStart;
    private DateTime nextClaimEnd;

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
                new DailyBonusData(0, 100, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(1, 150, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(2, 250, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(3, 500, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(4, 1000, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(5, 1500, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(6, 2500, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(7, 5000, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(8, 10000, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365)),
                new DailyBonusData(9, 25000, false, DateTime.UtcNow, DateTime.UtcNow + TimeSpan.FromDays(365))
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
        string json = JsonUtility.ToJson(new Bonuses(bonusesDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    private void CheckGetBonus()
    {
        currentDailyBonus = GetFirstNotClaimedBonus();
        nextClaimStart = DateTime.Parse(currentDailyBonus.NextClaimStart);
        nextClaimEnd = DateTime.Parse(currentDailyBonus.NextClaimEnd);

        OnCurrentClaimBonus?.Invoke(currentDailyBonus.Day);

        if (currentDateTime > nextClaimEnd)
        {
            RestartAll();
            currentDailyBonus = GetFirstNotClaimedBonus();
            return;
        }

        if (currentDateTime < nextClaimStart)
        {
            ostatokDateTime = nextClaimStart - currentDateTime;
            Coroutines.Start(Countdown_Coroutine());
            return;
        }

        if(currentDateTime >= nextClaimStart)
        {
            OnActivatedClaimButton?.Invoke();
        }
    }

    public void ClaimBonus()
    {
        OnDeactivatedClaimButton?.Invoke();

        currentDailyBonus.IsClaimed = true;
        OnAlreadyClaimBonus?.Invoke(currentDailyBonus.Day);

        if(currentDailyBonus.Day + 1 >= bonusesDatas.Count)
        {
            RestartAll();
            currentDailyBonus = GetFirstNotClaimedBonus();
        }
        else
        {
            currentDailyBonus = bonusesDatas[currentDailyBonus.Day + 1];
        }

        currentDailyBonus.NextClaimStart = (currentDateTime + TimeSpan.FromSeconds(10)).ToString();
        currentDailyBonus.NextClaimEnd = (currentDateTime + TimeSpan.FromDays(20)).ToString();

        CheckGetBonus();
    }

    private void RestartAll()
    {
        bonusesDatas.ForEach(Data => Data.IsClaimed = false);
    }

    private IEnumerator Countdown_Coroutine()
    {
        Debug.Log("ewdfef");

        TimeSpan timeRemaining = ostatokDateTime;

        while (true)
        {
            timeRemaining -= TimeSpan.FromSeconds(1);

            if (timeRemaining.TotalSeconds <= 0)
            {
                OnActivatedClaimButton?.Invoke();
                break;
            }

            Debug.Log(string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));
            OnCountdownTimer?.Invoke(string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));

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
