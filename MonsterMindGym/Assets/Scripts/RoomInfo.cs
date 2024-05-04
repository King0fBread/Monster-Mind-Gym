using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [SerializeField] private string _roomName;
    [SerializeField] private int _roomID;
    [SerializeField] private bool[] _availableDirectionsLeftRight = new bool[2];
    [SerializeField] private Transform _cameraTransform;

    public string GetRoomName()
    {
        return _roomName;
    }
    public int GetRoomID()
    {
        return _roomID;
    }
    public bool[] GetAvailableRoomDirections()
    {
        return _availableDirectionsLeftRight;
    }
    public Vector3 GetCamTransformPosition()
    {
        return _cameraTransform.position;
    }
}
