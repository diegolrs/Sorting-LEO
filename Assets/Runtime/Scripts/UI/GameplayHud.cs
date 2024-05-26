using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayHud : MonoBehaviour
{
    [SerializeField] MovementCounter _movementCounter;
    [SerializeField] Timer _timer;
    [SerializeField] TextMeshProUGUI _movementsText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] Animator _anim;

    const string OnMakeMovementAnim = "LED_Blink";
    int _lastMovementQuantity;

    private void Start() 
    {
        _lastMovementQuantity = _movementCounter.GetQuantity();
    }

    public string MovementCountToStr()
    {
        int count = _movementCounter.GetQuantity();

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

    public string TimeToSrt()
    {
        var elapsedTime = _timer.GetElapsedTime();
        int hours = Mathf.FloorToInt(elapsedTime / 3600);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    void UpdateMovementText()
    {
        _movementsText.text = MovementCountToStr();
    }

    void UpdateTimerText()
    {
        _timerText.text = TimeToSrt();
    }

    void ProcessMovementAnimation()
    {
        if(_movementCounter.GetQuantity() != _lastMovementQuantity)
        {
            _anim?.Play(OnMakeMovementAnim);
            _lastMovementQuantity = _movementCounter.GetQuantity();
        }
    }

    private void LateUpdate() 
    {
        UpdateMovementText();
        UpdateTimerText();
        ProcessMovementAnimation();
    }
}