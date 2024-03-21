using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DragInDrop))]
public class Card : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private DragInDrop dragInDrop;
    [SerializeField] private bool isStop;
    [SerializeField] private Image image;
    public void Init(string tag, Color color)
    {
        gameObject.tag = tag;
        image.color = color;
    }
    private void Start()
    {
        UpdateStop(false);
        dragInDrop.DragEvent += () => UpdateStop(true);
        dragInDrop.EndDragEvent += (_) => UpdateStop(false);
    }
    private void Update()
    {
        if (!isStop)
        {
            Move();
        }
    }

    private void UpdateStop(bool value)
    {
        isStop = value;
    }
    private void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
