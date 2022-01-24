using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThoughtControl : MonoBehaviour
{
    RectTransform rectTransform;
    TextMeshProUGUI text;
    private const float speed = 10f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        rectTransform.anchoredPosition3D += Vector3.up * speed * Time.deltaTime;
        text.color = new Color(1, 1, 1, Mathf.Lerp(text.color.a, 0, Time.deltaTime));
        if(text.color.a < 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
