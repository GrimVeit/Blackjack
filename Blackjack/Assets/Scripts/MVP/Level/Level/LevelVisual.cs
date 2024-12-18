using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelVisual
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Button buttonChooseLevel;
    [SerializeField] private Image imageLevel;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteInactive;

    public void Initialize()
    {
        buttonChooseLevel.onClick.AddListener(HandleClickToChooseLevelButton);
    }

    public void Dispose()
    {
        buttonChooseLevel.onClick.RemoveListener(HandleClickToChooseLevelButton);
    }

    public void SelectLevel()
    {
        imageLevel.sprite = spriteActive;
    }

    public void DeselectLevel()
    {
        imageLevel.sprite = spriteInactive;
    }

    #region Input

    public event Action<int> OnClickToChooseLevel;

    private void HandleClickToChooseLevelButton()
    {
        Debug.Log("Click to " +  id);

        OnClickToChooseLevel?.Invoke(id);
    }

    #endregion
}
