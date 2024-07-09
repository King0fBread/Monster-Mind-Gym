using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElixirsManager : MonoBehaviour
{
    [SerializeField] private GameObject _adblockObject;

    [SerializeField] private List<ElixirSlot> _elixirSlots;

    [SerializeField] private Sprite _graySprite;
    [SerializeField] private Sprite _blueSprite;
    [SerializeField] private Sprite _redSprite;
    [SerializeField] private Sprite _yellowSprite;

    private Dictionary<ElixirColors, Sprite> _colorToSpriteMap;

    private List<ElixirColors> _availableElixirColors;

    private void OnEnable()
    {
        //_adblockObject.SetActive(true);

        _availableElixirColors = new List<ElixirColors>()
        {
            ElixirColors.Gray, ElixirColors.Gray, ElixirColors.Gray, ElixirColors.Gray, ElixirColors.Gray,
            ElixirColors.Blue, ElixirColors.Blue,
            ElixirColors.Red,
            ElixirColors.Yellow,
        };

        _colorToSpriteMap = new Dictionary<ElixirColors, Sprite>
        {
            {ElixirColors.Gray, _graySprite },
            {ElixirColors.Blue, _blueSprite },
            {ElixirColors.Red, _redSprite },
            {ElixirColors.Yellow, _yellowSprite },
        };

        RandomlyPlaceElixirs();
    }
    private void RandomlyPlaceElixirs()
    {
        for(int i = 0; i< _availableElixirColors.Count; i++)
        {
            ElixirColors temp = _availableElixirColors[i];
            int randomIndex = Random.Range(i, _availableElixirColors.Count);
            _availableElixirColors[i] = _availableElixirColors[randomIndex];
            _availableElixirColors[randomIndex] = temp;
        }
        foreach(var color in _availableElixirColors)
        {
            print(color);
        }
        for(int i = 0; i<_elixirSlots.Count; i++)
        {
            ElixirColors color = _availableElixirColors[i];
            _elixirSlots[i].SetCurrentElixirColor(color, _colorToSpriteMap[color]);
        }
    }
}
