using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragInDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public event Action<DragInDrop> EndDragEvent;
    public event Action DragEvent;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;
    private Color _currentColor;

    private void Start()
    {
        _currentColor = image.color;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = Color.grey;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        DragEvent?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = _currentColor;
        EndDragEvent?.Invoke(this);
    }
}
