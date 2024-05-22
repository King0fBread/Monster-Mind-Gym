using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
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

        AssignButtons();
        GrowPattern();
        DisplayPattern();
    }
    private void GrowPattern()
    {
        HidePreviousPattern();

        if(_currentPatternSize >= _buttons.Length)
        {
            _currentPatternSize = _buttons.Length;
        }
        else
        {
            _currentPatternSize++;
        }

        _patternList.Clear();

        for(int i = 0; i < _currentPatternSize; i++)
        {
            Button _buttonToAdd;
            do
            {
                _buttonToAdd = _buttons[Random.Range(0, _buttons.Length)];
            } while (_patternList.Contains(_buttonToAdd));

            _patternList.Add(_buttonToAdd);
        }

        DisplayPattern();
    }
    private void HidePreviousPattern()
    {
        foreach(Button button in _patternList)
        {
            button.gameObject.SetActive(false);
        }
    }
    private void DisplayPattern()
    {
        foreach(Button button in _patternList)
        {
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TextMeshProUGUI>().text = (_patternList.IndexOf(button) + 1).ToString();
        }
    }
    private void HideNumbers()
    {
        foreach(Button button in _patternList)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
    private void ProcessButtonClick(Button button)
    {
        if(button == _patternList[_currentTapIndex])
        {
            button.gameObject.SetActive(false);
            if(_currentTapIndex == 1)
            {
                HideNumbers();
            }
            if(_currentTapIndex == _patternList.Count - 1)
            {
                _currentTapIndex = 0;
                GrowPattern();
                return;
            }

            _currentTapIndex++;
        }
        else
        {
            print("LOSE");
        }
    }
    private void AssignButtons()
    {
        foreach(Button button in _buttons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => ProcessButtonClick(button));
        }
    }
}
