using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipView : View
{
    public event Action<Chip> OnSpawn;

    public event Action<List<Chip>> OnRecallAllChips;
    public event Action<Chip> OnRetractLastChip;

    //[SerializeField] private Canvas canvas;
    //[SerializeField] private Transform parentSpawn;
    [SerializeField] private Transform transformBetCell;
    [SerializeField] private Chip chipPrefab;
    [SerializeField] private List<Chip> chipPrefabs = new List<Chip>();
    [SerializeField] private List<Chip> chips = new List<Chip>();
    [SerializeField] private List<Chip> usingChips = new List<Chip>();
    [SerializeField] private Transform transformDeleteChip;

    //[SerializeField] private Button recallAllChips;
    [SerializeField] private Button retractLastChip;

    public void Initialize()
    {
        //recallAllChips.onClick.AddListener(HandlerClickToRecallAllChips);
        retractLastChip.onClick.AddListener(HandlerClickToRetractLastChip);
    }

    public void Dispose()
    {
        //recallAllChips.onClick.RemoveListener(HandlerClickToRecallAllChips);
        retractLastChip.onClick.RemoveListener(HandlerClickToRetractLastChip);
    }

    public void SpawnChip(ChipData chipData, ICell cell, Vector2 vector)
    {
        Chip chip = Instantiate(chipPrefab, chipData.Parent);
        chip.transform.SetLocalPositionAndRotation(vector, chipPrefab.transform.rotation);
        chip.OnRetracted += OnRetractChip;
        chip.OnNoneRetracted += OnNoneRetracted;
        chip.OnFalled += OnFall;
        chip.Initialize(chipData, cell);

        chips.Add(chip);
    }

    public void SpawnChip(Chip chipPrefab)
    {
        Chip chip = Instantiate(chipPrefab, chipPrefab.transform.parent);
        //chip.transform.SetLocalPositionAndRotation(chipPrefab.transform.localPosition, chipPrefab.transform.rotation);
        chip.transform.SetLocalPositionAndRotation(Vector3.zero, chipPrefab.transform.rotation);
        chip.OnRetracted += OnRetractChip;
        chip.OnNoneRetracted += OnNoneRetracted;
        chip.OnFalled += OnFall;
        chip.Initialize(chipPrefab.ChipData);
        //chip.Move(transformBetCell.position);
        chip.Move(new Vector3(
            transformBetCell.position.x + UnityEngine.Random.Range(-0.3f, 0.3f),
            transformBetCell.position.y + UnityEngine.Random.Range(-0.3f, 0.3f),
            transformBetCell.position.z + UnityEngine.Random.Range(-0.3f, 0.3f)));

        chips.Add(chip);
    }

    public void RecallAllChips(List<Chip> chips)
    {
        for (int i = 0; i < chips.Count; i++)
        {
            chips[i].Retract();
        }
    }

    public void RetractLastChip(Chip chip)
    {
        chip.Retract();
    }

    public void NoneRetractChip(Chip chip)
    {
        chip.NoneRetract();
    }

    public void FallChip(Chip chip)
    {
        chip.Fall(transformDeleteChip.position);
    }

    public void FallenAllChips()
    {
        for (int i = 0; i < chips.Count; i++)
        {
            chips[i].Fall(transformDeleteChip.position);
        }
    }

    public void SpawnNumbers(float number)
    {
        usingChips.Sort((a, b) => b.ChipData.Nominal.CompareTo(a.ChipData.Nominal));

        float num = number;

        while(num > 0)
        {
            bool spawnedSomething = false;

            foreach (var prefab in usingChips)
            {
                if(num >= prefab.ChipData.Nominal)
                {
                    num -= prefab.ChipData.Nominal;
                    spawnedSomething = true;
                    //Debug.Log("Spawn nominal - " + prefab.ChipData.Nominal);
                    OnSpawn?.Invoke(prefab);
                }
            }

            if (!spawnedSomething)
            {
                Debug.Log("Ostatok:" + num);
                break;
            }
        }
    }


    public void ActivateChips(List<int> betNominals)
    {
        for (int i = 0; i < betNominals.Count; i++)
        {
            Debug.Log(betNominals[i]);

            for (int j = 0; j < chipPrefabs.Count; j++)
            {
                if (betNominals[i] == chipPrefabs[j].ChipData.Nominal)
                {
                    usingChips.Add(chipPrefabs[j]);
                }
            }
        }
    }

    public void RecallAllChips()
    {
        OnRecallAllChips?.Invoke(chips);
    }

    #region Input

    private void OnFall(Chip chip)
    {
        chip.OnRetracted -= OnRetractChip;
        chip.OnNoneRetracted -= OnNoneRetracted;
        chips.Remove(chip);
        Destroy(chip.gameObject);
    }

    private void OnRetractChip(Chip chip)
    {
        chip.OnRetracted -= OnRetractChip;
        chip.OnNoneRetracted -= OnNoneRetracted;
        chips.Remove(chip);
        Destroy(chip.gameObject);
    }

    private void OnNoneRetracted(Chip chip)
    {
        chip.OnRetracted -= OnRetractChip;
        chip.OnNoneRetracted -= OnNoneRetracted;
        chips.Remove(chip);
        Destroy(chip.gameObject);
    }

    private void HandlerClickToRetractLastChip()
    {
        if (chips.Count == 0) return;
        if (chips[chips.Count - 1].IsRestracted) return;
        OnRetractLastChip?.Invoke(chips[chips.Count - 1]);
    }

    #endregion
}
