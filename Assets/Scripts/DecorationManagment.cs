using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DecorationManagment : MonoBehaviour
{
    const string rendererPath = "Assets/Resources/SavedDecorations/SavedMeshRenderers/";
    const string filterPath = "Assets/Resources/SavedDecorations/SavedMeshFilters/";
    public string testingRenderer;

    public static void SaveJSON(MeshRenderer meshRenderer, MeshFilter meshFilter, string name)
    {
        string finalRendererPath = $"{rendererPath}{name}.txt";
        string finalFilterPath = $"{filterPath}{name}.txt";
        CheckIfFileExists(finalRendererPath);
        CheckIfFileExists(finalFilterPath);

        StreamWriter sw = new StreamWriter(finalRendererPath);
        sw.Write(EditorJsonUtility.ToJson(meshRenderer));
        sw.Close();

        sw = new StreamWriter(finalFilterPath);
        sw.Write(EditorJsonUtility.ToJson(meshFilter));
        sw.Close();
    }

    private static void CheckIfFileExists(string path)
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path,"empty");
        }
    }

    public static void LoadJSON(string nameOfObject, MeshRenderer rendererToOverride, MeshFilter filterToOverride)
    {
        string txt = ((TextAsset)Resources.Load($"SavedDecorations/SavedMeshFilters/{nameOfObject}.txt")).text;
        EditorJsonUtility.FromJsonOverwrite(txt, rendererToOverride);

        txt = ((TextAsset)Resources.Load($"SavedDecorations/SavedMeshFilters/{nameOfObject}.txt")).text;
        EditorJsonUtility.FromJsonOverwrite(txt, filterToOverride);
    }

    //[MenuItem("Asset Managment/Save MeshRenderer")]
    public static void SaveJSONEditor()
    {
        GameObject gameObject = Selection.activeGameObject;
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mr != null && mf != null)
        {
            SaveJSON(mr, mf, gameObject.name);
        }
    }
}

[CanEditMultipleObjects]
[CustomEditor(typeof(DecorationManagment))]
public class EditorTest : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Load temp JSON"))
        {
            DecorationManagment.LoadJSON(((DecorationManagment)target).testingRenderer,
                ((DecorationManagment)target).gameObject.GetComponent<MeshRenderer>(), 
                ((DecorationManagment)target).gameObject.GetComponent<MeshFilter>());
        }
    }
}

