using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Pagina : MonoBehaviour, IComparable
{
    TextMeshPro text;
    [TextArea]
    public string textoEspañol;
    [TextArea]
    public string textoIngles;
    public int numeroPagina;
    // Start is called before the first frame update
    public void Init()
    {
        text = GetComponent<TextMeshPro>();
        if(Manager.Instance.english){
            text.text = textoIngles;
        }else{
            text.text = textoEspañol;
        }
    }

    public void ChangeLanguage(){
        if(Manager.Instance.english){
            text.text = textoIngles;
        }else{
            text.text = textoEspañol;
        }
    }

    public int CompareTo(object obj)
    {
        if(obj == null) return 0;

        Pagina p = obj as Pagina;
        if(p.numeroPagina > numeroPagina){
            return -1;
        }else{
            return 1;
        }
    }
}
