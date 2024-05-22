using UnityEngine;

[CreateAssetMenu(menuName = "Data/BlockTypeSO", fileName = "New BlockTypeSO")]
public class BlockTypeSO : ScriptableObject
{
    [SerializeField] BlockType[] _types;
    [SerializeField] Color _defaultTextColor;
    [SerializeField] Color _defaultBgColor;

    private void Awake() 
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void ResetBlocks()
    {
        _types = new BlockType[(int)BlockType.PossibleValues.Size];

        for(int i = 0; i < (int)BlockType.PossibleValues.Size; i++)
        {
            _types[i] = new BlockType(i, _defaultTextColor, _defaultBgColor);
        }
    }

    public BlockType RandomBetween(BlockType.PossibleValues minValue, BlockType.PossibleValues maxValue)
    {
        int rand = Random.Range((int) minValue, (int) maxValue + 1);
        return GetWithValue((BlockType.PossibleValues) rand);
    }

    public BlockType GetWithValue(BlockType.PossibleValues value)
    {
        for(int i = 0; i < _types.Length; i++)
        {
            if(_types[i].Value == value)
                return _types[i];
        }

        throw new System.Exception($"Block type with value {value} not found");
    }
}
