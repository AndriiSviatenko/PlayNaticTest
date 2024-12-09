using UnityEngine;
using UnityEngine.UI;

public class ColorElement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private CardType cardType;
    public void Init(CardType cardType ,Color color)
    {
        this.cardType = cardType;
        image.color = color;
    }
    public CardType GetTypeCard() =>
       cardType;
}
