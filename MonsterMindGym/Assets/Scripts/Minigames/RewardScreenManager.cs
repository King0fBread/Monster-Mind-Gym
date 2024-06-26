using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _resultScreenObject;

    [SerializeField] private ElixirsManager _elixirsManager;

    [SerializeField] private TextMeshProUGUI _pointsValueText;
    [SerializeField] private TextMeshProUGUI _scoreValueText;

    public static RewardScreenManager instance { get { return _instance; } }
    private static RewardScreenManager _instance;
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void EnableRewardScreen(int earnedPoints, int bestLevelIfPresent = 0, double bestTimeIfPresent = 0.0f)
    {
        _resultScreenObject.SetActive(true);

        _pointsValueText.text = earnedPoints.ToString();

        if (bestLevelIfPresent != 0)
            _scoreValueText.text = "Level " + bestLevelIfPresent.ToString();
        else if (bestTimeIfPresent != 0.0f)
            _scoreValueText.text = "Best time is " + bestTimeIfPresent.ToString();

        _elixirsManager.gameObject.SetActive(true);
    }
}
