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
        switch(type){
            case "int":
                CreadorVariable.Instance.OpenModifyInt(intVariable,indice);
                CreadorObjetos.Instance.Close();
            break;
            case "float":
                CreadorVariable.Instance.OpenModifyFloat(floatVariable,indice);
                CreadorObjetos.Instance.Close();
            break;
            case "bool":
                CreadorVariable.Instance.OpenModifyBool(boolVariable,indice);
                CreadorObjetos.Instance.Close();
            break;
        }
    }

    public void Eliminar(){
        switch(type){
            case "int":
                CreadorObjetos.Instance.variablesInt.Remove(intVariable);
                Destroy(intVariable.gameObject);
            break;
            case "float":
                CreadorObjetos.Instance.variablesFloat.Remove(floatVariable);
                Destroy(floatVariable.gameObject);
            break;
            case "bool":
                CreadorObjetos.Instance.variablesBoolean.Remove(boolVariable);
                Destroy(boolVariable.gameObject);
            break;
        }

        CreadorObjetos.Instance.NumberVariables --;
        PanelIzquierdo.Instance.ReOrderVariable(indice);
    }
}
