using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private const int SPEED = 30;

    [SerializeField] private ViewCard _viewCard;
    [SerializeField] private bool isStop;
    private Data _data;
    [SerializeField] private RectTransform _position;
    
    public void Init(Data newData)
    {
        _data = newData;
        SetColor(_data.Color);
    }

    public CardType GetTypeCard() =>
        _data.CardType;

    public void SetParent(RectTransform parent)
    {
        _position.parent = parent;
    }

    public void SetColor(Color color) => 
        _viewCard.SetColor(color);

    public void SetPosition(Vector2 value) => 
        _position.anchoredPosition = value;
    public void Drag() => 
        UpdateStop(true);
    public void EndDrag() =>
        UpdateStop(false);

    private void Start()
    {
        UpdateStop(false);
    }

    private void Update()
    {
        if (!isStop)
        {
            Move();
        }
    }

    private void UpdateStop(bool value) => 
        isStop = value;
    private void Move() => 
        transform.Translate(Vector2.down * SPEED * Time.deltaTime);
}
