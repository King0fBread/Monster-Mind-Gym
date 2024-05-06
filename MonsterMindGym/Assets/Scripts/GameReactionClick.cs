using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Properties;
using UnityEngine.UI;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class GameReactionClick : MonoBehaviour
{
    [SerializeField] private Image _colorImage;
    [SerializeField] private Button _colorButton;

    [SerializeField] private Color _redColor;
    [SerializeField] private Color _greenColor;

    private float _timeUntilColorSwitch;
    private bool _colorIsClickable = false;

    private void OnEnable()
    {
        _colorButton.onClick.RemoveAllListeners();
        _colorButton.onClick.AddListener(CheckCurrentColor);

        _colorImage.color = _redColor;

        _timeUntilColorSwitch = Random.Range(1.5f, 6f);
    }
    private void CheckCurrentColor()
    {
        if (_colorIsClickable)
        {
            print("yeag");
        }
    }
    private void Update()
    {
        if(_timeUntilColorSwitch > 0)
        {
            _timeUntilColorSwitch -= Time.deltaTime;
        }
        else if(!_colorIsClickable)
        {
            _colorIsClickable = true;
            _colorImage.color = _greenColor;
        }
    }
}
