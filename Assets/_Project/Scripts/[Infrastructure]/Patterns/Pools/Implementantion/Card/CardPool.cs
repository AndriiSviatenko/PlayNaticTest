using Rayleigh.PrefabPool;
using UnityEngine;

public class CardPool
{
    private RectTransform _context;
    private CardsSO _cardsData;
    private PrefabPool _prefabPool;
    public CardPool(Card prefab, RectTransform context, CardsSO cardsData, int maxCapacity)
    {
        _context = context;
        _cardsData = cardsData;

        var poolParameters = new PoolParameters<Card>(maxCapacity);
        _prefabPool = new PrefabPool();
        _prefabPool.Configure(prefab, poolParameters);
    }

    public bool TryGet(Card card, Vector2 position, out Card result)
    {
        if (_prefabPool.TryGet(card, out result))
        {
            result.Init(GetRandomData());
            result.SetParent(_context);
            result.SetPosition(position);
            return true;
        }
        return false;
    }

    public void Prewarm(Card card, int count) =>
        _prefabPool.Prewarm(card, count);
    public void Release(Card card) =>
        _prefabPool.Release(card);
    private Data GetRandomData() =>
    _cardsData.Cards[Random.Range(0, _cardsData.Cards.Count)];
}