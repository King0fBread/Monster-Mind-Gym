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

    [SerializeField] private float _maxAmountOfPointsToObtain;

    [SerializeField] private float _achievedPointsPrecise;
    private int _achievedPointsRounded;

    private float _timeUntilColorSwitch;
    private bool _colorIsClickable = false;

    private int[] _totalAchievedPointsInThreeRounds;
    private int _currentRoundID;

    private void OnEnable()
    {
        _colorButton.onClick.RemoveAllListeners();
        _colorButton.onClick.AddListener(CheckCurrentColor);

        ResetRandomColorTimer();

        _achievedPointsPrecise = _maxAmountOfPointsToObtain;

        _totalAchievedPointsInThreeRounds = new int[3];

        _currentRoundID = 0;
    }
    private void CheckCurrentColor()
    {
        if (_colorIsClickable)
        {
            _colorIsClickable = false;

            _achievedPointsRounded = (int)Mathf.Round(_achievedPointsPrecise);
            _totalAchievedPointsInThreeRounds[_currentRoundID] = _achievedPointsRounded;

            if (_currentRoundID < _totalAchievedPointsInThreeRounds.Length - 1)
            {
                _currentRoundID++;
                ResetRandomColorTimer();
            }
            else
            {
                print("3 rounds");
            }

        }
    }
    private void ResetRandomColorTimer()
    {
        _timeUntilColorSwitch = Random.Range(1.5f, 6f);
        _colorImage.color = _redColor;
    }
    private void Update()
    {
        CountDownBeforeColorChange();

        DecreasePointsAfterColorChange();
    }
    private void CountDownBeforeColorChange()
    {
        if (_timeUntilColorSwitch > 0)
        {
            _timeUntilColorSwitch -= Time.deltaTime;
        }
        else if (!_colorIsClickable)
        {
            _colorIsClickable = true;
            _colorImage.color = _greenColor;
        }
    }
    private void DecreasePointsAfterColorChange()
    {
        if (_colorIsClickable)
        {
            _achievedPointsPrecise -= Time.deltaTime * 100f;
        }
    }
    
}
