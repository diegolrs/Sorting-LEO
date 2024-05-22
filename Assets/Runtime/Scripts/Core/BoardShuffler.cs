using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardShuffler : MonoBehaviour
{
    [SerializeField] ShiftManager _shiftManager;
    [SerializeField] BoardManager _boardManager;

    Vector2 _lastDirection = Vector2.zero;

    public void ShuffleBoard(int amount)
    {
        while(amount > 0)
        {
            var blockSpace = _boardManager.GetEmptyBlockSpace();

            var directions = _shiftManager.ShiftDirections.ToList();
            directions.Remove(_lastDirection);
            directions.Remove(-_lastDirection);
            directions.Shuffle();
            
            ShuffleOnce(blockSpace, directions);
            amount--;
        }
    }

    // Shuffle validates all possible directions of blocks surrounding empty space
    private void ShuffleOnce(BlockSpace blockSpace, List<Vector2> directions)
    {
        for(int i = 0; i < directions.Count; i++)
        {
            if(_boardManager.IsOutsideBoard(blockSpace.Position + directions[i]))
                continue;

            var block = _boardManager.GetBlockAtPosition(blockSpace.Position + directions[i]);
            if (block)
            {
                if(_shiftManager.CanMoveBlock(block, out var dir))
                {
                    _shiftManager.Shift(block, dir);
                    _lastDirection = dir;
                    return;
                }
            }                   
        }
    }
}