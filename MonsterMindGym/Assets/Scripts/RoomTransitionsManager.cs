using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomTransitionsManager : MonoBehaviour
{
    [SerializeField] private RoomInfo[] _rooms;
    [SerializeField] private int _initialRoomID;
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
        _camera.transform.position = _currentRoom.GetCamTransformPosition();
    }
}
