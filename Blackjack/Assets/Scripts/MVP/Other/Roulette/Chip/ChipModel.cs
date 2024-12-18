using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipModel
{
    public event Action OnRetractAllChips;
    public event Action OnFallenAllChips;

    public event Action<float> OnSpawnNumbers;

    public event Action OnRemoveAllChips;
    public event Action<int> OnRemoveChip;
    public event Action<int> OnAddChip;

    public event Action<List<Chip>> OnRecallAllChips;
    public event Action<Chip> OnNoneRetractChip;
    public event Action<Chip> OnRetractLastChip;
    public event Action<Chip> OnFallChip;

    public event Action<Chip> OnSpawnChip;
    public event Action<ChipData, ICell, Vector2> OnSpawn;

    public ISoundProvider soundProvider;

    public ChipModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void SpawnChip(ChipData chipData, ICell cell, Vector2 vector)
    {
        //soundProvider.PlayOneShot("ChipDrop");
        OnSpawn?.Invoke(chipData, cell, vector);
        OnAddChip?.Invoke(chipData.Nominal);
    }

    public void SpawnChip(Chip chip)
    {
        //soundProvider.PlayOneShot("ChipDrop");
        OnSpawnChip?.Invoke(chip);
        OnAddChip?.Invoke(chip.ChipData.Nominal);
    }

    public void RecallAllChips(List<Chip> chips)
    {
        if (chips.Count == 0) return;
        //soundProvider.PlayOneShot("ChipWhoosh");
        OnRecallAllChips?.Invoke(chips);
        OnRemoveAllChips?.Invoke();
    }

    public void RetractLastChip(Chip chip)
    {
        if (chip == null) return;
        //soundProvider.PlayOneShot("ChipWhoosh");
        OnRemoveChip?.Invoke(chip.ChipData.Nominal);
        OnRetractLastChip?.Invoke(chip);
    }

    public void RetractAllChips()
    {
        OnRetractAllChips?.Invoke();
    }

    public void FallenAllChips()
    {
        OnFallenAllChips?.Invoke();
    }

    public void NoneRetractChip(Chip chip)
    {
        OnNoneRetractChip?.Invoke(chip);
    }

    public void FallChip(Chip chip)
    {
        OnRemoveChip?.Invoke(chip.ChipData.Nominal);
        OnFallChip?.Invoke(chip);
    }

    public void SpawnNumbers(float numbers)
    {
        OnSpawnNumbers?.Invoke(numbers);
    }
}
