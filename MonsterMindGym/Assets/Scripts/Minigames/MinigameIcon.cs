using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameIcon : MonoBehaviour
{
    private Image _iconsImage;

    private Color32 _unlockedColor;
    private Color32 _lockedColor;

    private void Awake()
    {
        _unlockedColor = new Color32(180, 255, 148, 255);
        _lockedColor = new Color32(255, 141, 124, 255);

        _iconsImage = GetComponent<Image>();

        LockIconByDefault();
    }
    private void LockIconByDefault()
    {
        _iconsImage.color = _lockedColor;
    }
    public void UnlockIcon()
    {
        _iconsImage.color = _unlockedColor;
    }
}
