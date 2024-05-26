using UnityEngine;

public class GameEndValidator : MonoBehaviour
{
    [SerializeField] BoardManager _board;

    public bool PlayerWonGame()
    {
        var blockSpaces = _board.GetBlockSpaces();;

        // last square must be empty
        if(blockSpaces[blockSpaces.Count-1].IsHoldingBlock())
            return false;

        var orderedQueue = _board.GetOrderedBlocksQueue();
        for(int ix = 0; ix < blockSpaces.Count-2; ix++)
        {
            if(!blockSpaces[ix].IsHoldingBlock())
                return false;

            if(blockSpaces[ix].GetBlock().GetBlockType().Value != orderedQueue.Dequeue().Value)
                return false;
        }
        return true;
    }
}