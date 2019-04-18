using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class VariableObjeto : CustomAnchorable
{

    public string nombre;

    public string clase;

    public ObjetoBase objetoReferenciado;

    public LineRenderer referencia;

    public MeshRenderer meshClase;
    public MeshRenderer meshVariable;
    Material colorClase;
    public Material ColorClase { get { return colorClase; } set { colorClase = value; meshClase.material = colorClase; } }

    Material colorVariable;
    public Material ColorVariable { get { return colorVariable; } set { colorVariable = value; meshVariable.material = colorVariable; } }

    // Start is called before the first frame update
    public void Init(string nombre, string clase, CustomAnchor main, Material colorVariable, Material colorClase)
    {
        base.Init(main);
        this.nombre = nombre;
        this.clase = clase;
        this.colorVariable = colorVariable;
        this.colorClase = colorClase;
        objetoReferenciado = null;
        Interaction.OnGraspEnd += (() => Release());
        textoPanelSuperior.text = "<#"+ColorUtility.ToHtmlStringRGB(colorClase.color)+">"+clase +"</color>"+" " + "<#"+ColorUtility.ToHtmlStringRGB(colorVariable.color)+">"+nombre +"</color>"+ ";";
    }

    // Update is called once per frame
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

        if (objetoReferenciado != null && !Interaction.ignoreGrasping)
        {
            referencia.SetPosition(0, transform.position);
            referencia.SetPosition(1, objetoReferenciado.transform.position);
        }
        else
        {
            referencia.SetPosition(1, transform.position);
            referencia.SetPosition(0, transform.position);
        }
    }

    void Release()
    {
        if(Anchorable.anchor == null || Anchorable.anchor == MainAnchor){
            selected = false;
            Anchorable.anchor = MainAnchor as Anchor;
            Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
            Anchorable.isAttached = true;
            Anchorable.anchor.NotifyAttached(Anchorable);
        }else{
            selected = false;
            ExploracionObjeto.Instance.Open(this);
            MenuGrid.Instance.Close();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjetoBase o = other.gameObject.GetComponent<ObjetoBase>();

        if (o != null && o == objetoReferenciado)
        {
            return;
        }

        if (o != null && o.nombre == clase)
        {
            objetoReferenciado = o;
            Consola.Instance.Write("<#"+ColorUtility.ToHtmlStringRGB(colorClase.color)+">"+clase +"</color>"+" " + "<#"+ColorUtility.ToHtmlStringRGB(colorVariable.color)+">"+nombre +"</color> = new <#"+ColorUtility.ToHtmlStringRGB(colorClase.color)+">"+clase +"</color>" + "();");
        }
    }
}
