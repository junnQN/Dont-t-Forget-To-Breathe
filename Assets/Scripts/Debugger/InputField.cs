using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InputField : MonoBehaviour
{
    public string fieldName;
    public float value;

    public GameConfig gameConfig;

    public TMPro.TMP_InputField inputField;
    public TMPro.TMP_Text keyText;

    public string Value
    {
        get
        {
            return value.ToString();
        }
        set
        {
            this.value = float.Parse(value);
            UpdateFieldValue();
        }
    }

    public void UpdateInputField(string key, float value)
    {
        this.fieldName = key;
        this.value = value;

        keyText.text = key;
        inputField.text = value.ToString();
    }

    void UpdateFieldValue()
    {
        if (gameConfig == null)
        {
            return;
        }

        var type = gameConfig.GetType();
        var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            field.SetValue(gameConfig, value);
            Debug.Log("Field updated: " + fieldName);
        }
        else
        {
            Debug.LogError("Field not found: " + fieldName);
        }
    }
}
