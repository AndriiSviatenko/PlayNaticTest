using UnityEngine;

public class GenericFactory<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public GenericFactory(T prefab) => 
        Prefab = prefab;

    public virtual T GetNewInstance() =>
        Object.Instantiate(Prefab);
    public virtual T GetNewInstance(Vector2 position, Quaternion rotation) =>
        Object.Instantiate(Prefab, position,rotation);
}
