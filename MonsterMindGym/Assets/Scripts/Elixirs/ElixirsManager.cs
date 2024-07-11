using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

    private bool _hasClaimedElixir;

    public static ElixirsManager Instance { get { return _instance; } }
    private static ElixirsManager _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        _adblockObject.SetActive(true);

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

        SetUpElixirs();
    }
    private void SetUpElixirs()
    {
        foreach(var slot in _elixirSlots)
        {
            _hasClaimedElixir = false;
            slot.ShowCrate();

            RandomlyPlaceElixirs();

            //do after an ad
            SeeThroughRandomCrates();
        }
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
        for(int i = 0; i<_elixirSlots.Count; i++)
        {
            ElixirColors color = _availableElixirColors[i];
            _elixirSlots[i].SetCurrentElixirColor(color, _colorToSpriteMap[color]);
        }
    }
    public void ApplyPickedElixir(ElixirColors elixirColor)
    {
        if (_hasClaimedElixir)
        {
            return;
        }

        _hasClaimedElixir = true;

        foreach(var slot in _elixirSlots)
        {
            slot.HideCrate();
            //small explosion effect
        }
        MinigameRewardCalculator.instance.CalculateElixirMultipliedCurrency(GetMultiplier(elixirColor));
    }
    private int GetMultiplier(ElixirColors elixirColor)
    {
        switch (elixirColor)
        {
            case ElixirColors.Gray:
                return 1;
            case ElixirColors.Blue:
                return 2;
            case ElixirColors.Red:
                return 3;
            case ElixirColors.Yellow:
                return 4;
            default:
                return 1;
        }
    }
    private void SeeThroughRandomCrates()
    {
        List<ElixirSlot> pickedRandomSlots = new List<ElixirSlot>();

        for(int i = 0; i < 3; i++)
        {
            ElixirSlot pickedRandomSlot = _elixirSlots[Random.Range(0, _elixirSlots.Count)];
            if (!pickedRandomSlots.Contains(pickedRandomSlot))
            {
                pickedRandomSlots.Add(pickedRandomSlot);
            }
        }
        foreach(ElixirSlot slot in pickedRandomSlots)
        {
            slot.SeeThroughCrate();
        }
    }
    
}
