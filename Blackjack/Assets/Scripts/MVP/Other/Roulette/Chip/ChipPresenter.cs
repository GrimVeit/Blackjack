using System;
using UnityEngine;

public class ChipPresenter
{
    private ChipModel chipModel;
    private ChipView chipView;

    public ChipPresenter(ChipModel chipModel, ChipView chipView)
    {
        this.chipModel = chipModel;
        this.chipView = chipView;
    }

    public void Initialize()
    {
        ActivateEvents();

        chipView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        chipView.Dispose();
    }

    private void ActivateEvents()
    {
        chipView.OnRecallAllChips += chipModel.RecallAllChips;
        chipView.OnRetractLastChip += chipModel.RetractLastChip;
        chipView.OnSpawn += chipModel.SpawnChip;

        chipModel.OnRetractAllChips += chipView.RecallAllChips;
        chipModel.OnFallenAllChips += chipView.FallenAllChips;
        chipModel.OnSpawnNumbers += chipView.SpawnNumbers;
        chipModel.OnSpawn += chipView.SpawnChip;
        chipModel.OnSpawnChip += chipView.SpawnChip;
        chipModel.OnRecallAllChips += chipView.RecallAllChips;
        chipModel.OnRetractLastChip += chipView.RetractLastChip;
        chipModel.OnNoneRetractChip += chipView.NoneRetractChip;
        chipModel.OnFallChip += chipView.FallChip;
    }

    private void DeactivateEvents()
    {
        chipView.OnRecallAllChips -= chipModel.RecallAllChips;
        chipView.OnRetractLastChip -= chipModel.RetractLastChip;

        chipModel.OnSpawn -= chipView.SpawnChip;
        chipModel.OnRecallAllChips -= chipView.RecallAllChips;
        chipModel.OnRetractLastChip -= chipView.RetractLastChip;

        chipModel.OnNoneRetractChip -= chipView.NoneRetractChip;
        chipModel.OnFallChip -= chipView.FallChip;
    }

    #region Input

    public event Action OnRemoveAllChips
    {
        add { chipModel.OnRemoveAllChips += value; }
        remove { chipModel.OnRemoveAllChips -= value; }
    }

    public event Action<int> OnRemoveChip
    {
        add { chipModel.OnRemoveChip += value; }
        remove { chipModel.OnRemoveChip -= value; }
    }

    public event Action<int> OnAddChip
    {
        add { chipModel.OnAddChip += value; }
        remove { chipModel.OnAddChip -= value; }
    }

    public void SpawnChip(ChipData chipData, ICell cell, Vector2 vector) 
    {
        chipModel.SpawnChip(chipData, cell, vector);
    }

    public void RetractAllChips()
    {
        chipModel.RetractAllChips();
    }

    public void FallenAllChips()
    {
        chipModel.FallenAllChips();
    }

    public void NoneRetractChip(Chip chip)
    {
        chipModel.NoneRetractChip(chip);
    }

    public void FallChip(Chip chip)
    {
        chipModel.FallChip(chip);
    }

    public void SpawnNumbers(int number)
    {
        chipModel.SpawnNumbers(number);
    }

    #endregion
}
