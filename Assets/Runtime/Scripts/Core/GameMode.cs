using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMode : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] BoardManager _boardManager;
    [SerializeField] GameObject _gameOverHud;
    [SerializeField] GameObject _gameWonHud;
    [SerializeField] ShiftManager _shiftManager;
    [SerializeField] GameEndValidator _gameOverValidator;
    [SerializeField] SeedController _seedController;

    private bool _gameIsEnded;

    private void Start() 
    {
        _shiftManager.EnableShifts = true;
        _gameOverHud.SetActive(false);
        _gameWonHud.SetActive(false);
        _boardManager.GenerateBoard(_seedController.Seed);
        CenterCamera();
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
        // int freeSpacesCount = _boardManager.FreeSpacesCount();

        // if(freeSpacesCount >= 1)
        // {
        //     _boardManager.GenerateBlocks(1);
        //     freeSpacesCount--;
        // }

        // if(freeSpacesCount <= 0)
        // {
        //     if(_gameOverValidator.PlayerWonGame())
        //     {
        //         _gameIsEnded = true;
        //         OnGameWon();
        //     }
        //     else if (_gameOverValidator.PlayerLosedGame())
        //     {   
        //         _gameIsEnded = true;
        //         OnGameOver();  
        //     }
        // }
    }

    public void OnGameWon()
    {
        _shiftManager.EnableShifts = false;
        _gameWonHud.SetActive(true);
        Debug.LogWarning("Game Won");
    }

    public void OnGameOver()
    {
        _shiftManager.EnableShifts = false;
        _gameOverHud.SetActive(true);
        Debug.LogWarning("Game Over");
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void OnEndShift()
    {
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
