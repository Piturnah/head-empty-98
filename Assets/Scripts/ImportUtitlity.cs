using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImportUtitlity : MonoBehaviour
{
    [MenuItem("Asset Managment/Import New Wall Type")]
    public static void ImportNewWallType()
    {
        GameObject selectedObject = Selection.activeGameObject;
        MeshRenderer mr = selectedObject.GetComponent<MeshRenderer>();
        MeshFilter mf = selectedObject.GetComponent<MeshFilter>();
        if(mr != null && mf != null)
        {
            Assets assets = FindObjectOfType<Assets>();

            //Create new RoomType
            RoomType newRoomType = new RoomType()
            {
                wallMesh = mf.mesh,
                wallMaterials = mr.materials
            };

            //ScriptableObject newRoomType = ScriptableObject.CreateInstance(typeof(RoomType));
            
            //Save new RoomType
            AssetDatabase.CreateAsset(newRoomType,$"Assets/RoomTypes/{selectedObject.name}.asset");
            AssetDatabase.SaveAssets();

            //Add new RoomType to in scene Assets class
            //We should really be using a List 
        }
        else
        {
            Debug.LogError("Objet does not have required components");
        }
    }
}
