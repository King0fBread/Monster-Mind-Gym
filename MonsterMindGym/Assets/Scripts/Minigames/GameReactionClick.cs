using System;
using UnityEngine;
using TMPro;
using Unity.Properties;
using UnityEngine.UI;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;

public class GameReactionClick : MonoBehaviour, IFinishableGame
{
    [SerializeField] private Image _colorImage;
    [SerializeField] private Button _colorButton;

    [SerializeField] private Color _redColor;
    [SerializeField] private Color _greenColor;

    [SerializeField] private float _maxAmountOfPointsToObtain;

    private float _achievedPointsPrecise;
    private int _achievedPointsRounded;

    private float _timeUntilColorSwitch;
    private bool _colorIsClickable = false;

    private int[] _totalAchievedPointsInThreeRounds;
    private int _currentRoundID;

    private float _bestClickTime;
    private float _currentClickTime;

    private bool _lostRound;

    private void OnEnable()
    {
        _colorButton.onClick.RemoveAllListeners();
        _colorButton.onClick.AddListener(CheckCurrentColor);

        ResetRandomColorTimer();

        _achievedPointsPrecise = _maxAmountOfPointsToObtain;

        _totalAchievedPointsInThreeRounds = new int[3];

        _currentRoundID = 0;

        _bestClickTime = 100f;
        _currentClickTime = 0;

        _lostRound = false;
    }
    private void CheckCurrentColor()
    {
        if (_colorIsClickable)
        {
            _colorIsClickable = false;

            _achievedPointsRounded = (int)Mathf.Round(_achievedPointsPrecise);
            _totalAchievedPointsInThreeRounds[_currentRoundID] = _achievedPointsRounded;

            ResetRandomColorTimer();

            if (_currentRoundID < _totalAchievedPointsInThreeRounds.Length - 1)
            {
                RecordAndResetClickTime();
                _currentRoundID++;
            }
            else
            {
                FinishGameAndDisplayResult();
            }

        }
        else
        {
            _lostRound = true;
            FinishGameAndDisplayResult();
        }
    }
    public void FinishGameAndDisplayResult()
    {
        int totalPoints = 0;

        if (!_lostRound)
        {
            foreach (int roundPoints in _totalAchievedPointsInThreeRounds)
            {
                totalPoints += roundPoints;
            }
        }

        if (totalPoints < 1)
        {
            totalPoints = 1;
        }

        MinigameRewardCalculator.instance.CalculateInitialEarnedCurrency(totalPoints);
        RewardScreenManager.instance.EnableRewardScreen(totalPoints, bestTimeIfPresent: Math.Round(_bestClickTime,3));

        gameObject.SetActive(false);
    }
    private void RecordAndResetClickTime()
    {
        if (_currentClickTime < _bestClickTime)
        {
            _bestClickTime = _currentClickTime;
        }
        _currentClickTime = 0;
    }
    private void ResetRandomColorTimer()
    {
        _timeUntilColorSwitch = UnityEngine.Random.Range(1.5f, 6f);
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
            _currentClickTime += Time.deltaTime;

            _achievedPointsPrecise -= Time.deltaTime * 80f;
        }
    }
}
