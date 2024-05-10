using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class Debugger : MonoBehaviour
{
    public GameConfig gameConfig;

    public GameObject prefabObject;

    // Start is called before the first frame update
    void Start()
    {
        PrintAllFields(gameConfig);
    }

    void PrintAllFields(ScriptableObject obj)
    {
        var type = obj.GetType();
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields)
        {
            Debug.Log("Name: " + field.Name + " - Value: " + field.GetValue(obj));

            if (field.FieldType.IsArray)
            {
                var array = (Array)field.GetValue(obj);
                for (int i = 0; i < array.Length; i++)
                {
                    CreatePrefab(field.Name + " " + (i + 1), (float)array.GetValue(i));
                }
            }
            else
            {
                CreatePrefab(field.Name, (float)field.GetValue(obj));
            }
        }
    }

    void CreatePrefab(string key, float value)
    {
        var rt = GetComponent<RectTransform>();
        var input = Instantiate(prefabObject);
        input.transform.SetParent(transform);

        var inputRT = input.GetComponent<RectTransform>();
        var tempSizeDelta = rt.sizeDelta;
        tempSizeDelta.y += inputRT.sizeDelta.y;
        rt.sizeDelta = tempSizeDelta;

        input.transform.localScale = Vector3.one;
        var inputField = input.GetComponent<InputField>();
        inputField.UpdateInputField(key, value);
        inputField.gameConfig = gameConfig;
    }
}
