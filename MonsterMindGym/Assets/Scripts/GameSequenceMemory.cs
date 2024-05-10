using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSequenceMemory : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    [SerializeField] private Color _buttonDefaultColor;
    [SerializeField] private Color _buttonActivatedColor;

    private List<Button> _patternToRepeat;

    private bool _displayingPattern;

    private void OnEnable()
    {
        GrowPattern();
    }
    private void GrowPattern()
    {
        Button newPatternButton = _buttons[Random.Range(0, _buttons.Length - 1)];
        _patternToRepeat.Add(newPatternButton);

        _displayingPattern = true;
    }
}
