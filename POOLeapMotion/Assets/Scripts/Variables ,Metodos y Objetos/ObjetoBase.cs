using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Animation;
using Leap.Unity.Interaction;
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

    public bool expandido;

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

    ExploracionObjeto eo;
    MenuGrid mg;

    public new void Init(CustomAnchor main)
    {
        base.Init(main);

        eo = (ExploracionObjeto)Manager.Instance.GetMenu("ExploracionObjeto");
        mg = (MenuGrid)Manager.Instance.GetMenu("MenuGrid");
        
        Interaction.OnGraspEnd += (() => Release());
        material = cuerpo.material;
        IntVariable[] intVariables = GetComponentsInChildren<IntVariable>();

        int indexVar = 0;
        int indexMet = 0;

        foreach (IntVariable k in variablesInt)
        {
            anchorsVariables[indexVar].gameObject.SetActive(true);
            k.Init(this, anchorsVariables[indexVar]);
            k.transform.position = anchorsVariables[indexVar].transform.position;
            indexVar++;
        }
        foreach (FloatVariable k in variablesFloat)
        {
            anchorsVariables[indexVar].gameObject.SetActive(true);
            k.Init(this, anchorsVariables[indexVar]);
            k.transform.position = anchorsVariables[indexVar].transform.position;
            indexVar++;
        }
        foreach (BoolVariable k in variablesBool)
        {
            anchorsVariables[indexVar].gameObject.SetActive(true);
            k.Init(this, anchorsVariables[indexVar]);
            k.transform.position = anchorsVariables[indexVar].transform.position;
            indexVar++;
        }
        foreach (MetodoBase k in metodos)
        {
            anchorsMetodo[indexMet].gameObject.SetActive(true);
            k.Init(this, anchorsMetodo[indexMet]);
            k.transform.position = anchorsMetodo[indexMet].transform.position;
            indexMet++;
        }

        textoPanelSuperior.text = "new " + "<#" + ColorUtility.ToHtmlStringRGB(material.color) + ">" + nombre + "</color><#000000FF>();</color>";
        base.Init(main);
    }

    public void Expandir()
    {
        if (!expandido)
        {
            tween.PlayForward();
            expandido = true;
        }
    }

    public void Contraer()
    {
        if (expandido)
        {
            tween.PlayBackward();
            expandido = false;
        }
    }

    new void Update()
    {

        if (isOver && Input.GetMouseButton(0) && !Interaction.ignoreGrasping)
        {
            base.Update();
        }

        if (selected && isOver && Input.GetMouseButtonUp(0))
        {
            Interaction.OnGraspEnd();
        }
    }

    void Release()
    {
        StartCoroutine(ReleaseDelay());
    }

    public IEnumerator ReleaseDelay()
    {
        yield return null;
        if(Anchorable.anchor == Manager.Instance.inspeccionarVariable)
        {
            selected = false;
            eo.Open(this);
            mg.Close();
        }else if(Anchorable.anchor == Manager.Instance.papeleraExploradorObjetos){
            mg.RemoveOneObject(this);
        }else{
            selected = false;
            Anchorable.anchor = MainAnchor as Anchor;
            Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
            Anchorable.isAttached = true;
            Anchorable.anchor.NotifyAttached(Anchorable);
        }
    }

    public void LockItems(bool y)
    {
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
