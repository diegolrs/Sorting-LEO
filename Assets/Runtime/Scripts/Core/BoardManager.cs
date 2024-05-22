using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] BlockTypeSO _blockTypeSO;

    [Header("Board Parameters")]
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] int _amountOnStart;
    public int GetWidth() => _width;
    public int GetHeight() => _height;
    public Vector2 BoardCenter => new Vector2((_width -1)/2f, (_height-1)/2f);
    private Vector2 BoardSize => new Vector2(_width, _height);


    #region Block Data
    List<BlockSpace> _blockSpaces;
    List<Block> _blocks;
    public List<Block> GetBlocks() => _blocks;
    #endregion


    [Header("Prefabs")]
    [SerializeField] BlockSpace _blockSpacePrefab;
    [SerializeField] Block _blockPrefab;
    [SerializeField] BoardRenderer _boardRendererPrefab;


    #region Gizmos
    [Header("Gizmos")]
    [SerializeField] Color _gizmosColor = Color.blue;

    private void OnDrawGizmos() 
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawWireCube(BoardCenter, new Vector3(_width, _height));
    }
    #endregion
   
    public void GenerateBoard(int seed)
    {
        GenerateBoardRenderer();
        GenerateGrid();
        GenerateBlocks(_amountOnStart, seed);
    }
    
    private void GenerateBoardRenderer()
    {
        var board = Instantiate(_boardRendererPrefab, BoardCenter, Quaternion.identity, transform);
        board.transform.localScale = BoardSize;
    }

    public BlockSpace GetBlockSpaceAt(Vector2 position)
    {
         return _blockSpaces.Where(v => v.Position == position).FirstOrDefault();
    }

    private void GenerateGrid()
    {
        _blockSpaces = new List<BlockSpace>();

        for(int y = 0; y < _height; y++)
        {
            for(int x = 0; x < _width; x++)
            {
                var space = Instantiate(_blockSpacePrefab, new Vector3(x, y), Quaternion.identity, transform) as BlockSpace;
                _blockSpaces.Add(space);
            }
        }
    }

    public void GenerateBlocks(int amount, int seed)
    {
        if(_blocks == null)
            _blocks = new List<Block>();

        var freeSpaces = _blockSpaces
                            .Where(v => !v.IsHoldingBlock())
                            .OrderBy(r => Random.value);

        var possibleBlocks = GetRandomBlocks(seed);

        foreach(var freeSpace in freeSpaces.Take(amount))
        {
            var block = Instantiate(_blockPrefab, freeSpace.Position, Quaternion.identity, transform);
            block.SetType(possibleBlocks.Dequeue()); 
            block.EnterSpace(freeSpace);
            _blocks.Add(block);
        }
    }

    public void DestroyBlock(Block block)
    {
        if(_blocks != null && _blocks.Contains(block))
        {
            _blocks.Remove(block);
            Destroy(block.gameObject);
        }
    }

    public bool IsOutsideBoard(Vector2 position)
    {
        return position.x < 0 
               || position.x >= GetWidth()
               || position.y < 0
               || position.y >= GetHeight();
    }

    public bool HasBlockAtSpace(Vector2 position)
    {
        var space = GetBlockSpaceAt(position);
        return space != null && space.IsHoldingBlock();
    }

    public bool HasBlockAtSpace(Vector2 position, out Block blockAtSpace)
    {
        blockAtSpace = null;

        if(GetBlockSpaceAt(position) is BlockSpace bs)
            blockAtSpace = bs.GetBlock();

        return blockAtSpace != null;
    }

    ///<summary>Get Blocks from 1 to 15 randomly</summary>
    private Queue<BlockType> GetRandomBlocks(int seed)
    {
        var possibleBlocks = BlockType.PossibleValues
                             .GetValues(typeof(BlockType.PossibleValues))
                             .Cast<BlockType.PossibleValues>()
                             .ToList();
        possibleBlocks.Shuffle(seed);
        possibleBlocks.Remove(BlockType.PossibleValues.Size); // remove sizeofenum field

        var queue = new Queue<BlockType>();
        for(int i = 0; i < possibleBlocks.Count; i++)
        {
            queue.Enqueue(_blockTypeSO.GetWithValue(possibleBlocks[i]));
        }
        return queue;
    }
}