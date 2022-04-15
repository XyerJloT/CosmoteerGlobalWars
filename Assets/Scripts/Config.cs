using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Config : MonoBehaviour
{
    [SerializeField] public ShipTypeData[] _matchers;

    public IEnumerable<ShipTypeData> SortedMatchers
    {
        get
        {
            var list = new List<ShipTypeData>(_matchers);
            list.Sort();
            return list;
        }
    }
}

[Serializable]
public class ShipTypeData : IComparable<ShipTypeData>
{
    public Ship.RankType Type;
    public int Cost;
    public Sprite Sprite;

    public int CompareTo(ShipTypeData other)
    {
        return Cost.CompareTo(other.Cost);
    }
}

