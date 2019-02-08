using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreadorObjetos : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public TextMeshPro nombre;

    public GameObject menuVariables;
    public GameObject menuObjeto;

    public void TrimString(){
        nombreInput.text = nombreInput.text.Trim();
        nombre.text = nombreInput.text;
    }
}
