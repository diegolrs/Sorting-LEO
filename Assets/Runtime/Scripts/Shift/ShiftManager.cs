using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(ShiftInputs))]
public class ShiftManager : MonoBehaviour
{
    [SerializeField] GameMode _gameMode;
    [SerializeField] BoardManager _board;
    [SerializeField] ShiftInputs _input;
    public bool EnableShifts {get; set; }

    private bool _isShifting;

    public Vector2[] ShiftDirections = { Vector2.down, Vector2.up, Vector2.left, Vector2.right };

    void Update()
    {
        if(!EnableShifts || _isShifting)
            return;

        if (_input.BlockWasClicked(out var block))
        {
            if(CanMoveBlock(block, out var shiftDirection))
                Shift(block, shiftDirection);
        }
    }

    /// <param name="block">Candidate block to move</param>
    /// <param name="direction">Possible direction to move the block</param>
    public bool CanMoveBlock(in Block block, out Vector2 direction)
    {
        var position = block.Position;
        direction = Vector2.zero;

        if(block == null)
            return false;

        foreach(var shiftDirection in ShiftDirections)
        {
            if(!_board.IsOutsideBoard(position + shiftDirection) && !_board.HasBlockAtPosition(position+shiftDirection))
            {
                direction = shiftDirection;
                return true;
            }
        }

        return false;
    }

    public void Shift(Block block, Vector2 dir)
    {
        _isShifting = true;
        block.LeaveCurrentSpace();
        block.EnterSpace(_board.GetBlockSpaceAt(block.Position+dir));
        _gameMode.OnEndShift();
        _isShifting = false;
    }
}