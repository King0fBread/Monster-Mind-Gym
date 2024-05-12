using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSequenceMemory : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    [SerializeField] private Color _buttonDefaultColor;
    [SerializeField] private Color _buttonActivatedColor;

    [SerializeField] private float _delayBeforeDisplayingPattern;
    [SerializeField] private float _delayBeforeColorChange;

    private List<Button> _patternToRepeat;

    private bool _canDisplayPattern;
    private bool _canInteract;

    private int _currentClickID;

    private void OnEnable()
    {
        _patternToRepeat = new List<Button>();

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
        }


    }
}
