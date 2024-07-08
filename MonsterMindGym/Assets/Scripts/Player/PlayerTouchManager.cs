using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerTouchManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Canvas _screenSizeUICanvas;

    //private Camera _camera;
    //private Vector2 _worldPosition;

    [SerializeField] EventSystem _eventSystem;
    private GraphicRaycaster _graphicRaycaster;

    private void Awake()
    {
        _graphicRaycaster = _screenSizeUICanvas.GetComponent<GraphicRaycaster>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.SingleTouch.performed += SingleTouch_performed;
    }

    private void SingleTouch_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 touchPosition = obj.ReadValue<Vector2>();

        HandleUIClick(touchPosition);
    }
    private void HandleUIClick(Vector2 touchPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(_eventSystem)
        {
            position = touchPosition,
        };
        var results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(pointerEventData, results);

        print(results[0]);
    }
}
