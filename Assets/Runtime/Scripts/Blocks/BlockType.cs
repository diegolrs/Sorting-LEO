using UnityEngine;

[System.Serializable]
public struct BlockType
{
    public enum PossibleValues { _1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, Size }
    public PossibleValues Value;
    public Color BGColor;
    public Color TextColor;

    public BlockType(int valueIndex, Color textColor, Color bgColor)
    {
        this.Value = (PossibleValues)valueIndex;
        this.TextColor = textColor;
        this.BGColor = bgColor;
    }

    public int ToInt()
    {
        return (int)Value + 1;
    }
}