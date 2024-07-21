using TMPro;
using UnityEngine;

public class PlayerNotification : MonoBehaviour
{
    [SerializeField] private GameObject _notificationObject;
    [SerializeField] private GameObject _maxLevelNotificationObject;

    private TextMeshProUGUI _notificationText;
    private void Awake()
    {
        _notificationText = _notificationObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void DisplayNotification(string text)
    {
        _notificationText.text = text;
        _notificationObject.SetActive(true);

    }
    public void DisplayMaxLevelNotification()
    {
        _maxLevelNotificationObject.SetActive(true);
    }
}
