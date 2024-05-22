using UnityEngine;

public class ShiftInputs : MonoBehaviour
{
    [SerializeField] BoardManager _board;

    public bool BlockWasClicked(out Block clickedBlock)
    {
        clickedBlock = null;
        
        if (Input.GetMouseButtonDown(0))
        {
            foreach(var block in _board.GetBlocks())
            {
                if(block.IsMouseOver())
                {
                    clickedBlock = block;
                    return true;
                }
            }
        }

        return false;
    }
}