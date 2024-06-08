using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomTransitionsManager : MonoBehaviour
{
    [SerializeField] private RoomInfo[] _rooms;
    [SerializeField] private int _initialRoomID;

    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private Button[] _movementButtonsLeftRight = new Button[2];

    private RoomInfo _currentRoom;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        for (int i = 0; i < _rooms.Length; i++)
        {
            if (_rooms[i].GetRoomID() == _initialRoomID)
            {
                _currentRoom = _rooms[i];
                break;
            }
        }
    }
    private void Start()
    {
        SetCurrentRoomComponents();
    }
    private void SetCurrentRoomComponents()
    {
        StartCoroutine(SetCurrentRoomComponentsCoroutine());
    }
    public void MoveToLeftRoom()
    {
        StartCoroutine(SetCurrentRoomComponentsCoroutine());

        if (_currentRoom != null)
        {
            _currentRoom.GetComponent<IFunctionalArea>().ExecuteMechanicOnAreaExit();
        }

        _currentRoom = _rooms[_currentRoom.GetRoomID() - 1];

        _currentRoom.GetComponent<IFunctionalArea>().ExecuteMechanicOnAreaEntrance();

        SetCurrentRoomComponents();
    }
    public void MoveToRightRoom()
    {

        if (_currentRoom != null)
        {
            _currentRoom.GetComponent<IFunctionalArea>().ExecuteMechanicOnAreaExit();
        }

        _currentRoom = _rooms[_currentRoom.GetRoomID() + 1];

        _currentRoom.GetComponent<IFunctionalArea>().ExecuteMechanicOnAreaEntrance();

        SetCurrentRoomComponents();
    }
    private IEnumerator SetCurrentRoomComponentsCoroutine()
    {
        BlackoutScreenDelayManager.Instance.InitiateBlackoutDelay();

        yield return new WaitForSeconds(0.30f);

        _camera.transform.position = _currentRoom.GetCamTransformPosition();
        _roomNameText.text = _currentRoom.GetRoomName();

        for (int i = 0; i < _movementButtonsLeftRight.Length; i++)
        {
            _movementButtonsLeftRight[i].gameObject.SetActive(_currentRoom.GetAvailableRoomDirections()[i]);
        }
    }
}
