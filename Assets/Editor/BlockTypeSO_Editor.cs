using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockTypeSO))]
public class BlockTypeSO_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        BlockTypeSO blockType = (BlockTypeSO)target;

        DrawDefaultInspector();

        if(GUILayout.Button("Reset Blocks"))
        {
            blockType.ResetBlocks();
        }

        // Apply changes to the serialized object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(blockType);
        }
    }
}
