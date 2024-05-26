using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameWonHud : MonoBehaviour
{
    [SerializeField] GameplayHud _gameplayHud;
    [SerializeField] TextMeshProUGUI _movementsText;
    [SerializeField] TextMeshProUGUI _timerText;

    private void OnEnable() 
    {
        _movementsText.text = _gameplayHud.MovementCountToStr();
        _timerText.text = _gameplayHud.TimeToSrt();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneConstants.MenuScene);
    }
}
