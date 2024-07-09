using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElixirSlot : MonoBehaviour
{
    private ElixirColors _currentElixirColor;
    [SerializeField] private Image _elixirImage;
    public void SetCurrentElixirColor(ElixirColors elixirColor, Sprite elixirSprite)
    {
        _currentElixirColor = elixirColor;
        _elixirImage.sprite = elixirSprite;
    }
    public void PickElixir()
    {
        print("picked " + _currentElixirColor);
    }
}
