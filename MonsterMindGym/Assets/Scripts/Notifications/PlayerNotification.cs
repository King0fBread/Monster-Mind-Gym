using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private GameObject _maxLevelNotificationObject;
    public void DisplayNotification(string text)
    {
        _notificationText.text = text;

    }
    public void DisplayMaxLevelNotification()
    {
        _maxLevelNotificationObject.SetActive(true);
    }
}
