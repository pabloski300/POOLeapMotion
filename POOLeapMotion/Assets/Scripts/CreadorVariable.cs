using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreadorVariable : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public TextMeshPro nombre;
    
    string nivelDeProteccion = "";
    string tipo = "";

    public void TrimString(){
        nombreInput.text = nombreInput.text.Trim();
        nombre.text = nivelDeProteccion+" "+tipo+" "+nombreInput.text;
    }

    public void CambiarProteccion(string s){
        nivelDeProteccion = s;
        TrimString();
    }

    public void CambiarTipo(string s){
        tipo = s;
        TrimString();
    }
}
