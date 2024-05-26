using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayHud : MonoBehaviour
{
    [SerializeField] GameMode _gameMode;
    [SerializeField] MovementCounter _movementCounter;
    [SerializeField] Timer _timer;
    [SerializeField] TextMeshProUGUI _movementsText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] Animator _anim;

    [Header("Footer")]
    [SerializeField] GameObject _regularFooter;
    [SerializeField] GameObject _challengeFooter;

    [Header("Audio")]
    [SerializeField] AudioHandler _audioHandler;
    [SerializeField] AudioClip _movementFx;
    [SerializeField] AudioClip _loadSceneFx;

    const string OnMakeMovementAnim = "LED_Blink";
    int _lastMovementQuantity;

    public void GoToMainMenu()
    {
        print("cliquei");
        _gameMode.GoToMainMenu();
    }

    public void RestartGame()
    {
        _gameMode.RestartGame();
    }

    private void Start() 
    {
        _challengeFooter.SetActive(GameMode.Is_SDC32_Challenge);
        _regularFooter.SetActive(!GameMode.Is_SDC32_Challenge);
        _lastMovementQuantity = _movementCounter.GetQuantity();
        _audioHandler.PlaySFX(_loadSceneFx);
    }

    public string MovementCountToStr()
    {
        int count = _movementCounter.GetQuantity();

        if(count <= 0)
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
        if(_movementCounter.GetQuantity() != _lastMovementQuantity && _movementCounter.GetQuantity() > 0)
        {
            _anim?.Play(OnMakeMovementAnim);
            _lastMovementQuantity = _movementCounter.GetQuantity();
            _audioHandler.PlaySFX(_movementFx);
        }
    }

    private void LateUpdate() 
    {
        UpdateMovementText();
        UpdateTimerText();
        ProcessMovementAnimation();
    }
}