using UnityEngine;
using UnityEngine.UI;

public class ColorElement : MonoBehaviour
{
    [SerializeField] private Image image;
    public string Tag { get;private set; }
    public void Init(string tag,Color color)
    {
        Tag = tag;
        image.color = color;
    }
}
