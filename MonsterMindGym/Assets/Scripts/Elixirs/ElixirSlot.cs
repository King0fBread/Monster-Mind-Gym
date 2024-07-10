using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElixirSlot : MonoBehaviour
{
    [SerializeField] private Image _elixirImage;
    [SerializeField] private Image _crateImage;

    private Color _seeThroughCrateColor;
    private Color _hiddenCrateColor;
    private Color _visibleCrateColor;

    private ElixirColors _currentElixirColor;
    private void Awake()
    {
        _seeThroughCrateColor = new Color(1, 1, 1, 0.3f);
        _hiddenCrateColor = new Color(1, 1, 1, 0);
        _visibleCrateColor = new Color(1, 1, 1, 1);

        _crateImage.color = _visibleCrateColor;

    }
    public void SetCurrentElixirColor(ElixirColors elixirColor, Sprite elixirSprite)
    {
        _currentElixirColor = elixirColor;
        _elixirImage.sprite = elixirSprite;
    }
    public void PickElixir()
    {
        ElixirsManager.Instance.ApplyPickedElixir(_currentElixirColor);
    }
    public void SeeThroughCrate()
    {
        _crateImage.color = _seeThroughCrateColor;
    }
    public void HideCrate()
    {
        _crateImage.color = _hiddenCrateColor;
    }
}
