using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSequenceMemory : MonoBehaviour, ICountableLevelMinigame
{
    [SerializeField] private Button[] _buttons;

    [SerializeField] private Color _buttonDefaultColor;
    [SerializeField] private Color _buttonActivatedColor;

    [SerializeField] private float _delayBeforeDisplayingPattern;
    [SerializeField] private float _delayBeforeColorChange;

    [SerializeField] private TextMeshProUGUI _levelText;

    private int _currentLevel;

    private List<Button> _patternToRepeat;

    private bool _canDisplayPattern;
    private bool _canInteract;

    private int _currentClickID;

    private void OnEnable()
    {
        _patternToRepeat = new List<Button>();

        _currentLevel = 0;

        GrowPattern();
        AssignButtonClicks();

    }
    private void AssignButtonClicks()
    {
        foreach(Button button in _buttons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => CompareButtonClickWithPattern(button));
        }
    }
    private void GrowPattern()
    {
        _currentLevel++;
        DisplayCurrentLevel(_levelText, _currentLevel);

        Button newPatternButton = _buttons[Random.Range(0, _buttons.Length - 1)];
        _patternToRepeat.Add(newPatternButton);

        _canDisplayPattern = true;
    }
    private void Update()
    {
        if (_canDisplayPattern)
        {
            StartCoroutine(DisplayPatternCoroutine());
        }
    }
    private IEnumerator DisplayPatternCoroutine()
    {
        _canDisplayPattern = false;

        yield return new WaitForSeconds(_delayBeforeDisplayingPattern);

        foreach(Button button in _patternToRepeat)
        {
            button.image.color = _buttonActivatedColor;

            yield return new WaitForSeconds(_delayBeforeColorChange);

            button.image.color = _buttonDefaultColor;

            yield return new WaitForSeconds(_delayBeforeColorChange);
        }

        _canInteract = true;
        _currentClickID = 0;
    }
    private void CompareButtonClickWithPattern(Button clickedButton)
    {
        if (!_canInteract)
            return;

        if (clickedButton == _patternToRepeat[_currentClickID])
        {

            if(_currentClickID == _patternToRepeat.Count - 1)
            {
                _canInteract = false;
                GrowPattern();
            }
            else
            {
                _currentClickID++;
            }
        }
        else
        {
            print("LOSE");
            MinigameRewardCalculator.instance.CalculateInitialEarnedCurrency(_currentLevel * 16);
        }

    }

    public void DisplayCurrentLevel(TextMeshProUGUI levelTextObject, int currentLevel)
    {
        levelTextObject.text = "LEVEL " + currentLevel.ToString();
    }
}
