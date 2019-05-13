using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClaseSav
{
    public string nombre;
    public float[] color = new float[3];
    public List<AtributoSav> atributos = new List<AtributoSav>();
    public List<MetodoSav> metodos = new List<MetodoSav>();
}
