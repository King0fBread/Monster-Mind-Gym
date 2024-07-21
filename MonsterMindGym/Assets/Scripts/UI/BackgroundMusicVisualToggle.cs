using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicVisualToggle : MonoBehaviour
{
    [SerializeField] private Sprite _enabledBackgroundMusicSprite;
    [SerializeField] private Sprite _disabledBackgroundMusicSprite;
    private Image _buttonImage;

    private bool _shouldBeEnabled = true;
    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
    }
    public void TogglebackgroundMusicVisual()
    {
        _shouldBeEnabled = !_shouldBeEnabled;

        if (_shouldBeEnabled)
        {
            _buttonImage.sprite = _enabledBackgroundMusicSprite;
        }
        else
        {
            _buttonImage.sprite = _disabledBackgroundMusicSprite;
        }
    }
}
