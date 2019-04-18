using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Animation;
using UnityEngine;

public class ObjetoBase : CustomAnchorable
{

    public string nombre;

    public string codigo = "";


    public List<IntVariable> variablesInt;
    public List<FloatVariable> variablesFloat;
    public List<BoolVariable> variablesBool;

    public List<MetodoBase> metodos;

    public TransformTweenBehaviour tween;

    public Transform variablesParent;
    public CustomAnchor[] anchorsVariables;
    public Transform metodoParent;
    public CustomAnchor[] anchorsMetodo;

    bool expandido;

    Material material;

    public Material Material
    {
        get { return material; }
        set
        {
            material = value;
            cuerpo.material = material;
        }

    }

    public MeshRenderer cuerpo;

    public new void Init(CustomAnchor main)
    {
        material = cuerpo.material;
        IntVariable[] intVariables = GetComponentsInChildren<IntVariable>();

        int indexVar = 0;
        int indexMet = 0;

        foreach (IntVariable k in variablesInt)
        {
            anchorsVariables[indexVar].gameObject.SetActive(true);
            k.Init(this,anchorsVariables[indexVar]);
            k.transform.position = anchorsVariables[indexVar].transform.position;
            indexVar++;
        }
        foreach (FloatVariable k in variablesFloat)
        {
            anchorsVariables[indexVar].gameObject.SetActive(true);
            k.Init(this,anchorsVariables[indexVar]);
            k.transform.position = anchorsVariables[indexVar].transform.position;
            indexVar++;
        }
        foreach (BoolVariable k in variablesBool)
        {
            anchorsVariables[indexVar].gameObject.SetActive(true);
            k.Init(this,anchorsVariables[indexVar]);
            k.transform.position = anchorsVariables[indexVar].transform.position;
            indexVar++;
        }
        foreach (MetodoBase k in metodos)
        {
            anchorsMetodo[indexVar].gameObject.SetActive(true);
            k.Init(this,anchorsMetodo[indexMet]);
            k.transform.position = anchorsMetodo[indexMet].transform.position;
            indexMet++;
        }

        textoPanelSuperior.text = "new " + "<#"+ColorUtility.ToHtmlStringRGB(material.color)+">"+nombre +"</color><#000000FF>();</color>";
        base.Init(main);
    }

    public void Expandir(){
        if(!expandido){
            tween.PlayForward();
            expandido = true;
        }
    }

    public void Contraer(){
        if(expandido){
            tween.PlayBackward();
            expandido = false;
        }
    }

    public void LockItems(bool y){
        foreach (IntVariable k in variablesInt)
        {
            k.Interaction.ignoreGrasping = y;
        }
        foreach (FloatVariable k in variablesFloat)
        {
            k.Interaction.ignoreGrasping = y;
        }
        foreach (BoolVariable k in variablesBool)
        {
            k.Interaction.ignoreGrasping = y;
        }
        foreach (MetodoBase k in metodos)
        {
            k.Interaction.ignoreGrasping = y;
        }
    }

}
