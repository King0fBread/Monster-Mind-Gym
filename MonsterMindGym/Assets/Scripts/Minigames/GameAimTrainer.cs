using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameAimTrainer : MonoBehaviour
{
    [SerializeField] private Button _aimTargetButton;

    [SerializeField] private int _totalNumberOfTargets;

    [SerializeField] private Transform[] _aimTargetTransforms;

    private int _currentNumberOfTargets;

    private float _passedTime;

    private RectTransform _buttonRectTransform;

    private void OnEnable()
    {
        _aimTargetButton.onClick.RemoveAllListeners();
        _aimTargetButton.onClick.AddListener(DisplayNextTargetPosition);

        _buttonRectTransform = _aimTargetButton.GetComponent<RectTransform>();

        _currentNumberOfTargets = 0;

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
            print(_passedTime);

            Math.Round(_passedTime, 2);

            int roundedPoints = Convert.ToInt32( 4 / _passedTime * 150);
            MinigameRewardCalculator.instance.CalculateInitialEarnedCurrency(roundedPoints);
        }

    }
}
