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

    [SerializeField] private GameObject _roomNameRootObject;

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
        StartCoroutine(SetCurrentRoomComponentsCoroutine(isMovingToInitialRoom:true));
    }
    public void MoveToLeftRoom()
    {
        StartCoroutine(SetCurrentRoomComponentsCoroutine(isMovingToLeftRoom:true));
    }
    public void MoveToRightRoom()
    {
        StartCoroutine(SetCurrentRoomComponentsCoroutine(isMovingToLeftRoom:false));
    }
    private IEnumerator SetCurrentRoomComponentsCoroutine(bool isMovingToLeftRoom = false, bool isMovingToInitialRoom = false)
    {
        BlackoutScreenDelayManager.Instance.InitiateBlackoutDelay();

        SoundsManager.Instance.PlaySound(SoundsManager.Sounds.UIButtonSelect);

        yield return new WaitForSeconds(0.30f);

        if (isMovingToInitialRoom)
        {
            ExecuteInitalAreaEntranceMechanic();
        }
        else
        {
            ExecuteAreaExitEntranceMechanics(isMovingToLeftRoom);
        }

        _roomNameText.text = _currentRoom.GetRoomName();

        _roomNameRootObject.SetActive(false);
        _roomNameRootObject.SetActive(true);

        _camera.transform.position = _currentRoom.GetCamTransformPosition();

        for (int i = 0; i < _movementButtonsLeftRight.Length; i++)
        {
            _movementButtonsLeftRight[i].gameObject.SetActive(_currentRoom.GetAvailableRoomDirections()[i]);
        }
    }
    private void ExecuteAreaExitEntranceMechanics(bool isMovingToLeftRoom)
    {
        int roomIdChange = isMovingToLeftRoom ? -1 : 1;

        if (_currentRoom != null)
        {
            _currentRoom.GetComponent<IFunctionalArea>().ExecuteMechanicOnAreaExit();
        }

        _currentRoom = _rooms[_currentRoom.GetRoomID() + roomIdChange];

        _currentRoom.GetComponent<IFunctionalArea>().ExecuteMechanicOnAreaEntrance();
    }
    private void ExecuteInitalAreaEntranceMechanic()
    {
        _currentRoom.GetComponent<IFunctionalArea>().ExecuteMechanicOnAreaEntrance();
    }
}
