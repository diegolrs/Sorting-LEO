using System.Collections.Generic;
using UnityEngine;

public class MovementCounter : MonoBehaviour
{
    int _quantity = -1; //TODO: IGNORE FIRST MOVEMENT ON SUFFLE 

    public void IncreaseOne()
    {
        _quantity += 1;
    }

    public int GetQuantity()
    {
        return _quantity;
    }
}