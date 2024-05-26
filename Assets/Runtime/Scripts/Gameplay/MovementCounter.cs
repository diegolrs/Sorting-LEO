using System.Collections.Generic;
using UnityEngine;

public class MovementCounter : MonoBehaviour
{
    int _quantity;

    public void IncreaseOne()
    {
        _quantity += 1;
    }

    public int GetQuantity()
    {
        return _quantity;
    }
}