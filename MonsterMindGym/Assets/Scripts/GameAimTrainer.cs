using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAimTrainer : MonoBehaviour
{
    [SerializeField] private Button _aimTargetButton;

    private float _targetPositionX;
    private float _targetPositionY;

    private void OnEnable()
    {
        Camera cam = Camera.main;

        _targetPositionX = Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).x, cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        _targetPositionY = Random.Range(cam.ScreenToWorldPoint(new Vector2(0, 0)).y, cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);

        _aimTargetButton.transform.position = new Vector2(_targetPositionX, _targetPositionY);
    }
}
