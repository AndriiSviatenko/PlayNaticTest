using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsData", menuName = "SO/CardsData", order = 0)]
public class CardsSO : ScriptableObject
{
    public List<Data> Cards;
}
