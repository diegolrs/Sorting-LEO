using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMode : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] Camera _mainCamera;
    [SerializeField] BoardManager _boardManager;
    [SerializeField] BoardShuffler _boardShuffler;
    [SerializeField] GameEndValidator _gameOverValidator;
    [SerializeField] SeedController _seedController;
    [SerializeField] Timer _timer;

    [Header("Shift")]
    [SerializeField] ShiftManager _shiftManager;
    [SerializeField] ShiftInputs _shiftInputs;
    [SerializeField] MovementCounter _movementCounter;

    [Header("HUD")]
    [SerializeField] GameObject _gameWonHud;

    private bool _gameIsEnded;

    private void Start() 
    {
        // HUD
        CenterCamera();
        _gameWonHud.SetActive(false);

        // Shift Controllers
        _shiftManager.EnableShifts = true;
        _shiftInputs.EnableInputs = false;

        // Board Generation
        _boardManager.GenerateBoard();
        Debug.Log("Seed: " + _seedController.Seed);
        _boardShuffler.ShuffleBoard();

        // Allow Gameplay
        _shiftInputs.EnableInputs = true;
        _timer.Enabled = true;
    }

    private void CenterCamera()
    {
        Vector3 cameraPosition = _boardManager.BoardCenter;
        cameraPosition.z = _mainCamera.transform.position.z;
        _mainCamera.transform.position = cameraPosition;
    }

    public int GetSeed() => 42;

    public void ValidateEndOfGame()
    {
        if(_gameOverValidator.PlayerWonGame())
        {
            _gameIsEnded = true;
            OnGameWon();
        }
    }

    public void OnGameWon()
    {
        _shiftManager.EnableShifts = false;
        _timer.Enabled = false;
        _shiftInputs.EnableInputs = false;
        _gameWonHud.SetActive(true);
        Debug.LogWarning("Game Won");
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void OnEndShift()
    {
        _movementCounter.IncreaseOne();
        StartCoroutine(EndShiftRoutine());
    }

    private IEnumerator EndShiftRoutine()
    {
        _shiftManager.EnableShifts = false;
        yield return StartCoroutine(MoveBlocksRoutine());
        ValidateEndOfGame();
        _shiftManager.EnableShifts = !_gameIsEnded;
    }

    private IEnumerator MoveBlocksRoutine()
    {
        foreach(var block in _boardManager.GetBlocks())
            block.GoToBlockSpacePosition();

        yield return new WaitForSeconds(BlockAnimationController.SlideAnimationTime);
    }
}
