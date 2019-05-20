using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmenuMetodo : MonoBehaviour
{
    public string nombre;
    public List<TMP_InputField> inputs;
    public TextMeshPro output;

    public enum DefaultType
    {
        String,
        Int,
        Float
    }
    public DefaultType defaultType;
    public List<int> defaultValueInt;
    public List<string> defaultValueString;
    public List<float> defaultValueFloat;

    public bool midExecution;

    ExploracionMetodo e;


    // Start is called before the first frame update
    public void Init()
    {
        e = (ExploracionMetodo)Manager.Instance.GetMenu("ExploracionMetodo");

        if (name == "MenuRead")
        {
            foreach (TMP_InputField t in inputs)
            {
                t.gameObject.SetActive(false);
            }
        }
        
        Clear();
    }

    public void Clear()
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            switch (defaultType)
            {
                case DefaultType.Int:
                    inputs[i].text = defaultValueInt[i].ToString();
                    break;
                case DefaultType.Float:
                    inputs[i].text = defaultValueFloat[i].ToString();
                    break;
                case DefaultType.String:
                    inputs[i].text = defaultValueString[i];
                    break;
            }
            inputs[i].DeactivateInputField();
        }
        if (output != null)
        {
            output.text = "";
        }
    }

    public void TrimString(int i)
    {
        if (inputs[i].text.Trim() == "")
        {
            e.GetButton("Ejecutar").gameObject.SetActive(false);
            return;
        }

        if (!midExecution)
        {
            e.GetButton("Ejecutar").gameObject.SetActive(true);
        }

        if (defaultType != DefaultType.String)
        {
            inputs[i].text = inputs[i].text.Trim();
        }

        if (inputs[i].text.Length > inputs[i].characterLimit)
        {
            inputs[i].text = inputs[i].text.Remove(inputs[i].characterLimit, 1);
        }


    }
    public void InputsReady()
    {
        Debug.Log(inputs[0].text);
        if (inputs[0].text != "")
        {
            e.metodoActual.inputsReady = true;
        }
    }
}
