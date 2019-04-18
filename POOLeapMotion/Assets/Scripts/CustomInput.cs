using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomInput : MonoBehaviour
{
    TMP_InputField input;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<TMP_InputField>();
    }


    public void Select(){
        input.ActivateInputField();
        input.stringPosition = input.text.Length;
        Debug.Log("seleccionar");
    }
}
