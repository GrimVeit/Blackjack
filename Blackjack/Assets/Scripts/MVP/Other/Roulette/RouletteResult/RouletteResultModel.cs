using System;

public class RouletteResultModel
{
    public event Action OnStartShowResult;
    public event Action OnFinishHideResult;

    private RouletteSlotValue rouletteSlotValue;

    public void ShowResult(RouletteSlotValue rouletteSlotValue)
    {
        this.rouletteSlotValue = rouletteSlotValue;
    }
}
