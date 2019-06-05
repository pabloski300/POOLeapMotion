using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class VariableObjeto : CustomAnchorable
{
    public string nombre;
    public string nombreColor;
    string nombreColorClase;
    public string clase;
    public ObjetoBase objetoReferenciado;
    public LineRenderer referencia;
    public MeshRenderer meshClase;
    public MeshRenderer meshVariable;

    Material colorClase;
    public Material ColorClase { get { return colorClase; } set { colorClase = value; meshClase.material = colorClase; } }
    Material colorVariable;
    public Material ColorVariable { get { return colorVariable; } set { colorVariable = value; meshVariable.material = colorVariable; } }

    ExploracionObjeto eo;
    MenuGrid mg;
    Consola c;


    public void Init(string nombre, string clase, CustomAnchor main, Material colorVariable, Material colorClase)
    {
        base.Init(main);

        eo = (ExploracionObjeto)Manager.Instance.GetMenu("ExploracionObjeto");
        mg = (MenuGrid)Manager.Instance.GetMenu("MenuGrid");
        c = (Consola)Manager.Instance.GetMenu("Consola");

        this.nombre = nombre;
        this.clase = clase;
        this.colorVariable = colorVariable;
        this.colorClase = colorClase;
        this.nombreColor = "<#" + ColorUtility.ToHtmlStringRGB(colorVariable.color) + ">" + nombre + "</color>";
        this.nombreColorClase = "<#" + ColorUtility.ToHtmlStringRGB(colorClase.color) + ">" + clase + "</color>";
        objetoReferenciado = null;
        Interaction.OnGraspEnd += (() => Release());
        textoPanelSuperior.text = nombreColorClase + " " + nombreColor + ";";
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

        if (objetoReferenciado != null && !objetoReferenciado.expandido)
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
        StartCoroutine(ReleaseDelay());
    }

    public IEnumerator ReleaseDelay()
    {
        yield return null;

        if (Anchorable.anchor == Manager.Instance.inspeccionarVariable)
        {
            if (objetoReferenciado != null)
            {
                selected = false;
                eo.Open(this);
                mg.Close();
            }
            else
            {
                selected = false;
                Anchorable.anchor = MainAnchor as Anchor;
                Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
                Anchorable.isAttached = true;
                Anchorable.anchor.NotifyAttached(Anchorable);
                transform.position = Anchorable.anchor.transform.position;
                if (Manager.Instance.english)
                {
                    mg.ShowText("Please asign an object to the variable");
                }
                else
                {
                    mg.ShowText("Por favor asigna un objeto a la variable");
                }
            }
        }
        else if (Anchorable.anchor == Manager.Instance.papeleraExploradorObjetos)
        {
            mg.RemoveOneVariable(this);
        }
        else
        {
            selected = false;
            Anchorable.anchor = MainAnchor as Anchor;
            Anchorable.anchorLerpCoeffPerSec = MainAnchor.LerpCoeficient;
            Anchorable.isAttached = true;
            Anchorable.anchor.NotifyAttached(Anchorable);
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
            c.Write(nombreColor+" = new "+nombreColorClase + "();");
        }
    }
}
