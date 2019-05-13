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
    public TextMeshPro nombre;
    

    [HideInInspector]
    public int indice;

    public RepresentacionMetodo cube;
    public CustomAnchor mainAnchor;

    PanelIzquierdo pi;
    CreadorMetodos cm;
    CreadorObjetos co;


    public void Init(){
        pi = (PanelIzquierdo)Manager.Instance.GetMenu("PanelIzquierdo");
        cm = (CreadorMetodos)Manager.Instance.GetMenu("CreadorMetodos");
        co = (CreadorObjetos)Manager.Instance.GetMenu("CreadorObjetos");

        modificar.OnPress += (()=>Modificar());
        this.gameObject.SetActive(false);
        cube.Init(mainAnchor,this);
    }

    public void Modificar(){
        pi.HideButtons();
        cm.OpenModify(metodo,indice);
        co.Close();
    }

    public void Eliminar(){
        co.metodos.Remove(metodo.nombre);
        Destroy(metodo.gameObject);
        pi.ReOrderMetodo(indice);
        co.NumberMethods --;
    }
}
