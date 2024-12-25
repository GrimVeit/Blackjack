using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelVisualizeView : View
{
    [SerializeField] private Image imageLevelBackground;
    [SerializeField] private Image imageLevelDescription;
    [SerializeField] private GameObject buttonPlay;

    public void SetLevel(Level level, bool isActive)
    {
        imageLevelBackground.sprite = level.SpriteBackgroundLevel;
        imageLevelDescription.sprite = level.SpriteDescriptionLevel;

        ActivateButtonPlay(isActive);
    }

    public void ActivateButtonPlay(bool isActive)
    {
        if(isActive)
            buttonPlay.SetActive(true);
        else
            buttonPlay.SetActive(false);
    }
}
