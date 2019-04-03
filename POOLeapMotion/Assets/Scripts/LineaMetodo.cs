using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class LineaMetodo : MonoBehaviour
{

    [HideInInspector]
    public MetodoBase metodo;

    public InteractionButton modificar;
    public InteractionButton eliminar;
    public TextMeshPro nombre;

    [HideInInspector]
    public int indice;

    public void Start(){
        modificar.OnPress += (()=>Modificar());
        eliminar.OnPress += (()=>Eliminar());
        this.gameObject.SetActive(false);
    }

    public void Modificar(){
        PanelIzquierdo.Instance.HideButtons();
        CreadorMetodos.Instance.OpenModify(metodo,indice);
        CreadorObjetos.Instance.Close();
    }

    public void Eliminar(){
        CreadorObjetos.Instance.metodos.Remove(metodo.nombre);
        Destroy(metodo.gameObject);
        PanelIzquierdo.Instance.ReOrderMetodo(indice);
        CreadorObjetos.Instance.NumberMethods --;
    }
}
