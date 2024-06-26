using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameIconsDisplayer : MonoBehaviour
{
    private Image _iconsImage;

    private Color32 _unlockedColor;
    private Color32 _lockedColor;

    private void Awake()
    {
        _unlockedColor = new Color32(180, 255, 148, 255);
        _lockedColor = new Color32(255, 141, 124, 255);

        _iconsImage = GetComponent<Image>();

        LockByDefault();
    }
    private void LockByDefault()
    {
        _iconsImage.color = _lockedColor;
    }
}
