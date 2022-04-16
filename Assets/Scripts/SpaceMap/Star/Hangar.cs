using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar
{
    private List<Ship> _content = new List<Ship>();

    public void Put(Ship ship)
    {
        _content.Add(ship);
    }
}
