using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayHud : MonoBehaviour
{
    [SerializeField] MovementCounter _movementCounter;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Animator _anim;

    const string OnMakeMovementAnim = "LED_Blink";
    int _lastMovementQuantity;

    private void Start() 
    {
        _lastMovementQuantity = _movementCounter.GetQuantity();
    }

    string MovementCountToStr(int count)
    {
        if(count == 0)
            return "-";

        string str = "";

        float divisor = 10000000; // 8 digits
        while(count < divisor)
        {
            str += "0";
            divisor /= 10;
        }

        return str + count;
    }

    private void LateUpdate() 
    {
        int curMovementQuantity = _movementCounter.GetQuantity();
        _text.text = MovementCountToStr(curMovementQuantity);
        if(curMovementQuantity != _lastMovementQuantity)
        {
            _anim?.Play(OnMakeMovementAnim);
            _lastMovementQuantity = curMovementQuantity;
        }
    }
}