using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ThoughtsManagment : MonoBehaviour
{
    private Canvas self;
    private static RectTransform _instance;

    private void Awake()
    {
        _instance = GetComponent<RectTransform>();
        self = GetComponent<Canvas>();
    }

    public static void CreateThought()
    {
        StreamReader reader = new StreamReader("Assets/Resources/thoughts.json");
        Debug.Log(reader.ReadToEnd());

        GameObject newThought = (GameObject)Instantiate(Resources.Load("Thought"), Vector3.zero, Quaternion.identity);
        newThought.GetComponent<RectTransform>().SetParent(_instance);
        //newThought.GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-_instance.sizeDelta.x/2,_instance.sizeDelta.x/2),Random.Range(-_instance.sizeDelta.y/2, _instance.sizeDelta.y/2));
        //newThought.GetComponent<TextMeshProUGUI>().text = 
    }
}
