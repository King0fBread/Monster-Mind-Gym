using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
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
            int randomPointID = Random.Range(0, _aimTargetTransforms.Length-1);
            _buttonRectTransform.position = _aimTargetTransforms[randomPointID].position;
        }
        else
        {
            print(_passedTime);
        }

    }
}
