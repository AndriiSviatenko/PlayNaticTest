using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInDrop : MonoBehaviour
{
    public event Action<Card> EndDragEvent;
    private Camera _mainCamera;
    private Card _currentCard;
    private InputProvider _inputProvider;
    private List<RaycastResult> _raycastResults = new();

    public void Init() => 
        _inputProvider = new InputProvider();

    private void Awake()
    { 
        Init();
    }

    private void Update()
    {
        if (_inputProvider.MouseClickDown())
        {
            TryStartDrag();
        }

        if (_inputProvider.MouseClick() && _currentCard != null)
        {
            Drag();
        }

        if (_inputProvider.MouseClickUp() && _currentCard != null)
        {
            EndDrag();
        }
    }

    private void TryStartDrag()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        _raycastResults.Clear();
        EventSystem.current.RaycastAll(pointerEventData, _raycastResults);

        Debug.Log($"Raycast results count: {_raycastResults.Count}");
        foreach (var result in _raycastResults)
        {
            Debug.Log($"Raycast hit: {result.gameObject.name}");
            if (result.gameObject.TryGetComponent(out Card card))
            {
                _currentCard = card;
                _currentCard.Drag();
                Debug.Log($"Dragging card: {_currentCard.name}");
                break;
            }
        }
    }

    private void Drag()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _currentCard.transform.parent as RectTransform,
            Input.mousePosition,
            null,
            out Vector2 localPoint
        );

        _currentCard.SetPosition(localPoint);
    }

    private void EndDrag()
    {
        EndDragEvent?.Invoke(_currentCard);
        _currentCard.EndDrag();
        _currentCard = null;
    }
}
