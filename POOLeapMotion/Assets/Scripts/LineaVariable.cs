using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class LineaVariable : MonoBehaviour
{
    [HideInInspector]
    public string type;

    [HideInInspector]
    public IntVariable intVariable;
    [HideInInspector]
    public FloatVariable floatVariable;
    [HideInInspector]
    public BoolVariable boolVariable;

    public InteractionButton modificar;
    public TextMeshPro nombre;

    [HideInInspector]
    public int indice;

    public RepresentacionAtributo cube;
    public CustomAnchor mainAnchor;

    PanelIzquierdo pi;
    CreadorAtributos ca;
    CreadorObjetos co;

    public void Init(){
        pi = (PanelIzquierdo)Manager.Instance.GetMenu("PanelIzquierdo");
        ca = (CreadorAtributos)Manager.Instance.GetMenu("CreadorAtributos");
        co = (CreadorObjetos)Manager.Instance.GetMenu("CreadorObjetos");

        modificar.OnPress += (()=>Modificar());
        this.gameObject.SetActive(false);
        
        cube.Init(mainAnchor,this);
    }

    public void Modificar(){
        pi.HideButtons();
        switch(type){
            case "int":
                ca.OpenModifyInt(intVariable,indice);
                co.Close();
            break;
            case "float":
                ca.OpenModifyFloat(floatVariable,indice);
                co.Close();
            break;
            case "bool":
                ca.OpenModifyBool(boolVariable,indice);
                co.Close();
            break;
        }
    }

    public void Eliminar(){
        switch(type){
            case "int":
                co.variablesInt.Remove(intVariable);
                Destroy(intVariable.gameObject);
            break;
            case "float":
                co.variablesFloat.Remove(floatVariable);
                Destroy(floatVariable.gameObject);
            break;
            case "bool":
                co.variablesBoolean.Remove(boolVariable);
                Destroy(boolVariable.gameObject);
            break;
        }

        co.NumberVariables --;
        pi.ReOrderVariable(indice);
    }
}
