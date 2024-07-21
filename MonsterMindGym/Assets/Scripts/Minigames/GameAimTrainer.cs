using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameAimTrainer : MonoBehaviour, IFinishableGame
{
    [SerializeField] private Button _aimTargetButton;

    [SerializeField] private int _totalNumberOfTargets;

    [SerializeField] private Transform[] _aimTargetTransforms;

    private int _currentNumberOfTargets;

    private int _roundedPoints;

    private float _passedTime;

    private RectTransform _buttonRectTransform;

    private void OnEnable()
    {
        _aimTargetButton.onClick.RemoveAllListeners();
        _aimTargetButton.onClick.AddListener(DisplayNextTargetPosition);

        _buttonRectTransform = _aimTargetButton.GetComponent<RectTransform>();

        _currentNumberOfTargets = 0;

        _passedTime = 0;

    }
    private void Update()
    {
        _passedTime += Time.deltaTime;
    }
    private void DisplayNextTargetPosition()
    {
        
        _currentNumberOfTargets++;

        if(_currentNumberOfTargets < _totalNumberOfTargets)
        {
            int randomPointID = UnityEngine.Random.Range(0, _aimTargetTransforms.Length-1);
            _buttonRectTransform.position = _aimTargetTransforms[randomPointID].position;
        }
        else
        {
            Math.Round(_passedTime, 2);

            _roundedPoints = Convert.ToInt32( 6 / _passedTime * 150);
            FinishGameAndDisplayResult();
        }

    }

    public void FinishGameAndDisplayResult()
    {
        MinigameRewardCalculator.instance.CalculateInitialEarnedCurrency(_roundedPoints);
        RewardScreenManager.instance.EnableRewardScreen(_roundedPoints, bestTimeIfPresent: Math.Round(_passedTime, 3));

        gameObject.SetActive(false);
    }
}
