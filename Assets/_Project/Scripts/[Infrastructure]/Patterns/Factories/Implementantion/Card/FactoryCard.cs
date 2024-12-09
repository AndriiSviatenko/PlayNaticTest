using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCard : GenericFactory<Card>
{
    private RectTransform _content;
    private CardsSO _cardsData;

    public FactoryCard(Card prefab) : base(prefab)
    {
    }

    public void Init(RectTransform content, CardsSO cardsData)
    {
        _content = content;
        _cardsData = cardsData;
    }

    public override Card GetNewInstance(Vector2 position, Quaternion rotation)
    {
        var instance = base.GetNewInstance(position, rotation);
        instance.Init(GetRandomData());
        instance.SetParent(_content);
        return instance;
    }

    private Data GetRandomData() =>
        _cardsData.Cards[Random.Range(0, _cardsData.Cards.Count)];
}
