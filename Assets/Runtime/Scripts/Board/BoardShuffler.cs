using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardShuffler : MonoBehaviour
{
    [SerializeField] GameMode _gameMode;
    [SerializeField] ShiftManager _shiftManager;
    [SerializeField] BoardManager _boardManager;
    [SerializeField] SeedController _seedController;
    [SerializeField] int _shuffleAmount = 100;

    Block lastBlockMoved;

    private void Awake()  => lastBlockMoved = null;

    public void ShuffleBoard()
    {
        while(_shuffleAmount > 0)
        {
            var blockSpace = _boardManager.GetEmptyBlockSpace();

            var directions = _shiftManager.ShiftDirections.ToList();
            directions.Shuffle();
            
            ShuffleOnce(blockSpace, directions);
            _shuffleAmount--;
        }
        _gameMode.OnEndShift();
    }

    private bool CanShuffleOnPosition(BlockSpace emptyBlockSpace, Vector2 direction)
    {
        if(_boardManager.IsOutsideBoard(emptyBlockSpace.Position + direction))
            return false;

        var block = _boardManager.GetBlockAtPosition(emptyBlockSpace.Position + direction);
        if(block == null || block == lastBlockMoved)
            return false;

        return true;
    }

    // Shuffle validates all possible directions of blocks surrounding empty space
    private void ShuffleOnce(BlockSpace emptyBlockSpace, List<Vector2> directions)
    {
        for(int i = 0; i < directions.Count; i++)
        {
            if(!CanShuffleOnPosition(emptyBlockSpace, directions[i]))
                continue;

            var block = _boardManager.GetBlockAtPosition(emptyBlockSpace.Position + directions[i]);
            _shiftManager.Shift(block, -directions[i]);
            lastBlockMoved = block;
            return;          
        }
    }

    private void Update() 
    {
        if(Application.isEditor && Input.GetKeyDown(KeyCode.S))
        {
            _shuffleAmount = 100;
            ShuffleBoard();
        }
    }
}