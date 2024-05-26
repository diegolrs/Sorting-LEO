using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameWonHud : MonoBehaviour
{
    [SerializeField] GameplayHud _gameplayHud;
    [SerializeField] TextMeshProUGUI _movementsText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] GameObject _challengeFooter;

    [Header("Audio")]
    [SerializeField] AudioHandler _audioHandler;
    [SerializeField] AudioClip _clickButtonFx;
    [SerializeField] AudioClip _winningFx;

    private void OnEnable() 
    {
        _movementsText.text = _gameplayHud.MovementCountToStr();
        _timerText.text = _gameplayHud.TimeToSrt();
        _audioHandler.PlaySFX(_winningFx);
        _challengeFooter.SetActive(GameMode.Is_SDC32_Challenge);
    }

    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuRoutine());
    }

    IEnumerator GoToMainMenuRoutine()
    {
        _audioHandler.PlaySFX(_clickButtonFx);
        yield return new WaitForSeconds(0.25f); // audio time
        SceneManager.LoadScene(SceneConstants.MenuScene);
    }
}
