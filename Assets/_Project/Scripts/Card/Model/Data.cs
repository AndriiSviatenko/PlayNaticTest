using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "SO/CardData", order = 0)]
public class Data : ScriptableObject
{
    public CardType CardType;
    public Color Color;

    private void OnValidate()
    {
        SetColor();
    }

    public void SetColor()
    {
        switch (CardType)
        {
            case CardType.Red:
                Color = Color.red;
                break;
            case CardType.Green:
                Color = Color.green;
                break;
            case CardType.Blue:
                Color = Color.blue;
                break;
            case CardType.Yellow:
                Color = Color.yellow;
                break;
            case CardType.Magenta:
                Color = Color.magenta;
                break;
            case CardType.White:
                Color = Color.white;
                break;
            case CardType.Cyan:
                Color = Color.cyan;
                break;
            case CardType.Gray:
                Color = Color.gray;
                break;
            case CardType.Grey:
                Color = Color.grey;
                break;
            case CardType.Black:
                Color = Color.black;
                break;
            default:
                Color = Color.black;
                break;  
        }
    }
}
public enum CardType
{
    Red,
    Green, 
    Blue,
    Yellow,
    Magenta,
    White,
    Cyan,
    Gray,
    Grey,
    Black
}
