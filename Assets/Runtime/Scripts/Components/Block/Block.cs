using TMPro;
using UnityEngine;

[RequireComponent(typeof(BlockAnimationController))]
public class Block : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] TextMeshPro _text;
    [SerializeField] BlockTypeSO _blockTypeSO;
    [SerializeField] BlockAnimationController _anim;
    
    private BlockType _type;

    private void OnDestroy() => LeaveCurrentSpace();

    public Vector2 Position => transform.localPosition;
    public bool CompareValue(BlockType.PossibleValues value) => _type.Value == value;

    private BlockSpace _blockSpace;

    public void SetType(BlockType type)
    {
        _type = type;
        ApplyOnUI(_type);
    }

    private void ApplyOnUI(BlockType type)
    {
        _renderer.color = type.BGColor;
        _text.text = type.ToInt().ToString();
        _text.color = type.TextColor;
    }

    public void EnterSpace(BlockSpace space)
    {
        _blockSpace = space;
        _blockSpace.HoldBlock(this);
    }

    public void GoToBlockSpacePosition()
    {
        if(_blockSpace != null)
        {
            _anim.MakeSlideAnimation(_blockSpace.Position);
        }
    }

    public void LeaveCurrentSpace()
    {
        if(_blockSpace != null)
        {
            _blockSpace.ReleaseBlock(this);
            _blockSpace = null;
        }
    }

    public bool IsMouseOver()
    {
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _renderer.bounds.Contains(mouse_position);
    }
}