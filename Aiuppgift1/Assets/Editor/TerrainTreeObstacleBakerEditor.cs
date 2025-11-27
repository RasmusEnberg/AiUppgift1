using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainTreeObstacleBaker))]
public class TerrainTreeObstacleBakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var baker = (TerrainTreeObstacleBaker)target;
        if (GUILayout.Button("Rebuild Tree Obstacles"))
        {
            baker.Rebuild();
        }
    }
}

