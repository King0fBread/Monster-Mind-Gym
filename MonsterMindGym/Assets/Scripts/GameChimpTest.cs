using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameChimpTest : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    private List<Button> _patternList;

    private int _currentPatternSize;
    private int _currentTapIndex;
    private void OnEnable()
    {
        _patternList = new List<Button>();

        _currentPatternSize = 2;
        _currentTapIndex = 0;

        GrowPattern();
        DisplayPattern();
    }
    private void GrowPattern()
    {
        _currentPatternSize++;

        for(int i = 0; i < _currentPatternSize; i++)
        {
            Button _buttonToAdd;
            do
            {
                _buttonToAdd = _buttons[Random.Range(0, _buttons.Length - 1)];
            } while (_patternList.Contains(_buttonToAdd));

            _patternList.Add(_buttonToAdd);
        }
    }
    private void DisplayPattern()
    {
        foreach(Button button in _patternList)
        {
            button.gameObject.SetActive(true);
        }
    }
}
